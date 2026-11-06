using MermaidSharp.Configs;
using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Diagrams
{
    /// <summary>
    /// Represents a Mermaid Git graph diagram, including branches, commits, merges, and checkouts.
    /// </summary>
    /// <remarks>Use this class to construct and render Git-style diagrams by programmatically adding
    /// branches, commits, merges, and checkouts. The resulting graph can be used to generate Mermaid diagrams for
    /// documentation or visualization purposes. The class enforces branch existence and naming constraints to ensure
    /// valid Git graph construction.</remarks>
    public class GitGraph : AGraph<GitGraphConfig>
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        protected override string Name => "gitGraph";

        private readonly List<AGitAction> _actions = new List<AGitAction>();
        private readonly List<string> _branches = new List<string>();
        private readonly List<string> _tags = new List<string>();
        private readonly Dictionary<string, string> _commitBranches = new Dictionary<string, string>();
        private string _currentBranch;

        /// <summary>
        /// Gets the name of the main branch used by the configuration.
        /// </summary>
        public string MainBranch
        {
            get
            {
                var mainBranchName = Config?.MainBranchName;
                return string.IsNullOrWhiteSpace(mainBranchName) ? "main" : mainBranchName;
            }
        }

        /// <summary>
        /// Gets the name of the branch on which the next commit will be added.
        /// </summary>
        public string CurrentBranch => _currentBranch ?? MainBranch;

        /// <summary>
        /// Gets or sets the direction of the Git operation.
        /// </summary>
        public GitDirection Direction { get; set; }

        /// <summary>
        /// Gets the collection of actions associated with this instance.
        /// </summary>
        public IReadOnlyList<AGitAction> Actions => _actions;

        /// <summary>
        /// Gets the collection of branch names associated with the current instance.
        /// </summary>
        public IReadOnlyList<string> Branches => _branches;

        /// <summary>
        /// Gets the collection of tag names associated with the current instance.
        /// </summary>
        public IReadOnlyList<string> Tags => _tags;

        /// <summary>
        /// Initializes a new instance of the GitGraph class with the specified title and configuration.
        /// </summary>
        /// <param name="title">The title to display for the Git graph. If not specified, the title is empty.</param>
        /// <param name="direction">The direction of the Git graph. If not specified, the direction is set to None.</param>
        /// <param name="config">The configuration settings to apply to the Git graph. If null, default settings are used.</param>
        public GitGraph(string title = "", GitDirection direction = GitDirection.None, GitGraphConfig config = null)
            : base(title, config)
        {
            Direction = direction;
        }

        /// <summary>
        /// Generates the complete Mermaid diagram as a formatted string.
        /// </summary>
        /// <returns>A string containing the full Mermaid diagram.</returns>
        public override string CalculateDiagram()
        {
            var lines = new List<string>();
            lines.Add(GetHeaderString());
            var name = Name;
            if (Direction != GitDirection.None)
                name += $" {Direction.PrimaryString()}:";
            lines.Add(name);

            lines.AddRange(Actions.Select(n => n.ToString()).Indent());

            return string.Join(Environment.NewLine, lines.ClearNewLines());
        }

        /// <summary>
        /// Creates a new branch in the Git graph with the specified name and switches to it, as Mermaid does.
        /// </summary>
        /// <param name="branch">The name of the branch to create. Cannot be null, empty or already exist.</param>
        /// <returns>The current GitGraph instance, enabling method chaining.</returns>
        public GitGraph Branch(string branch)
        {
            if (string.IsNullOrWhiteSpace(branch))
                throw new ArgumentException("Branch name cannot be null or whitespace.", nameof(branch));

            if (BranchExists(branch))
                throw new InvalidOperationException($"Branch '{branch}' already exists.");

            _actions.Add(new GitBranch(branch));
            _branches.Add(branch);
            _currentBranch = branch;

            return this;
        }

        /// <summary>
        /// Switches the current working branch to the specified branch in the Git graph.
        /// </summary>
        /// <param name="branch">The name of the branch to switch to. Cannot be null, empty, or consist only of white-space characters.</param>
        /// <returns>The current GitGraph instance, enabling method chaining.</returns>
        public GitGraph Checkout(string branch)
        {
            EnsureBranchExists(branch);

            _actions.Add(new GitCheckout(branch));
            _currentBranch = branch;
            return this;
        }

        /// <summary>
        /// Adds a commit to the graph with the specified identifier and an optional tag.
        /// </summary>
        /// <param name="id">The unique identifier for the commit to add.</param>
        /// <param name="tag">An optional tag to associate with the commit. If not specified, no tag is added.</param>
        /// <param name="commitType">The type of the commit, which may affect its visual representation in the diagram.</param>
        /// <returns>The current instance of the graph, enabling method chaining.</returns>
        public GitGraph Commit(string id, string tag = "", GitCommitType commitType = GitCommitType.None)
        {
            EnsureCommitIdIsUnique(id);

            _actions.Add(new GitCommit(id, tag, commitType));

            RegisterCommit(id, tag);
            return this;
        }

        /// <summary>
        /// Merges the specified branch into the current branch, optionally applying a tag to the merge operation.
        /// </summary>
        /// <param name="branch">The name of the branch to merge into the current branch. Must refer to an existing branch or the main
        /// branch, and must differ from the current branch.</param>
        /// <param name="id">The unique identifier for the merge commit to add.</param>
        /// <param name="tag">An optional tag to associate with the merge. If specified, the tag is added to the graph.</param>
        /// <param name="commitType">The type of the commit, which may affect its visual representation in the diagram.</param>
        /// <returns>The current GitGraph instance, enabling method chaining.</returns>
        public GitGraph Merge(string branch, string id = "", string tag = "", GitCommitType commitType = GitCommitType.None)
        {
            EnsureBranchExists(branch);

            if (branch == CurrentBranch)
                throw new InvalidOperationException($"Cannot merge branch '{branch}' into itself.");

            EnsureCommitIdIsUnique(id);

            _actions.Add(new GitMerge(branch, id, tag, commitType));

            RegisterCommit(id, tag);
            return this;
        }

        /// <summary>
        /// Applies a cherry-pick of the specified commit to the current branch.
        /// </summary>
        /// <remarks>
        /// When <paramref name="id"/> refers to a merge commit (matched by its id in the graph),
        /// the parent id is resolved automatically by finding the last commit with an id on the merged branch
        /// before the merge occurred.
        /// </remarks>
        /// <param name="id">The identifier of an existing commit from another branch. Cannot be null or whitespace.</param>
        /// <param name="tag">An optional tag to associate with the cherry-pick operation. If specified, the tag is added to the graph.</param>
        /// <returns>The current GitGraph instance, enabling method chaining.</returns>
        public GitGraph CherryPick(string id, string tag = "")
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Commit ID cannot be null or whitespace.", nameof(id));

            if (!_commitBranches.TryGetValue(id, out var sourceBranch))
                throw new InvalidOperationException($"Cannot cherry-pick '{id}': commit does not exist.");

            if (sourceBranch == CurrentBranch)
                throw new InvalidOperationException($"Cannot cherry-pick '{id}': the commit is already on the current branch '{CurrentBranch}'.");

            var parentId = string.Empty;
            var mergeReference = Actions.OfType<GitMerge>().FirstOrDefault(m => m.Id == id);
            if (mergeReference != null)
            {
                // For a merge commit, the parent must be the last commit with an id on the merged branch
                parentId = FindLastCommitOnBranch(mergeReference.Branch, mergeReference);

                if (string.IsNullOrWhiteSpace(parentId))
                    throw new InvalidOperationException(
                        $"Cannot cherry-pick merge '{id}': no commit with an id found on branch '{mergeReference.Branch}'. To cherry-pick a merge, ensure that the merged branch has at least one commit with an id.");
            }

            _actions.Add(new GitCherryPick(id, parentId, tag));

            if (!string.IsNullOrWhiteSpace(tag))
                _tags.Add(tag);
            return this;
        }

        private bool BranchExists(string branch)
        {
            return branch == MainBranch || _branches.Contains(branch);
        }

        private void EnsureBranchExists(string branch)
        {
            if (string.IsNullOrWhiteSpace(branch))
                throw new ArgumentException("Branch name cannot be null or whitespace.", nameof(branch));

            if (!BranchExists(branch))
                throw new InvalidOperationException($"Branch '{branch}' does not exist. Please create it first using Branch('{branch}').");
        }

        private void EnsureCommitIdIsUnique(string id)
        {
            if (!string.IsNullOrWhiteSpace(id) && _commitBranches.ContainsKey(id))
                throw new InvalidOperationException($"Commit '{id}' already exists.");
        }

        private void RegisterCommit(string id, string tag)
        {
            if (!string.IsNullOrWhiteSpace(id))
                _commitBranches[id] = CurrentBranch;

            if (!string.IsNullOrWhiteSpace(tag))
                _tags.Add(tag);
        }

        /// <summary>
        /// Finds the id of the last commit made on the specified branch before the given action.
        /// </summary>
        /// <param name="branchName">The name of the branch to search commits on.</param>
        /// <param name="beforeAction">The action at which to stop the search (exclusive).</param>
        /// <returns>The id of the last commit found on the branch, or an empty string if none is found.</returns>
        private string FindLastCommitOnBranch(string branchName, AGitAction beforeAction)
        {
            var currentBranch = MainBranch;
            var lastCommitId = string.Empty;

            foreach (var action in _actions)
            {
                if (action == beforeAction)
                    break;

                string commitId = null;
                if (action is GitBranch branch)
                    currentBranch = branch.Branch;
                else if (action is GitCheckout checkout)
                    currentBranch = checkout.Branch;
                else if (action is GitCommit commit)
                    commitId = commit.Id;
                else if (action is GitMerge merge)
                    commitId = merge.Id;

                if (currentBranch == branchName && !string.IsNullOrWhiteSpace(commitId))
                    lastCommitId = commitId;
            }

            return lastCommitId;
        }
    }
}
