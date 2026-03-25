using MermaidSharp.Configs;
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
    public class GitGraph : AGraph
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        public override string Name => "gitGraph";
        /// <summary>
        /// Gets the configuration settings specific to the Git graph rendering.
        /// </summary>
        public new GitGraphConfig Config => base.Config as GitGraphConfig;

        private List<AGitAction> _actions = new List<AGitAction>();
        private List<string> _branches = new List<string>();
        private List<string> _tags = new List<string>();
        private List<string> _commits = new List<string>();

        /// <summary>
        /// Gets the name of the main branch used by the configuration.
        /// </summary>
        public string MainBranch => Config?.MainBranchName ?? "main";
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
        /// <param name="config">The configuration settings to apply to the Git graph. If null, default settings are used.</param>
        public GitGraph(string title = "", GitGraphConfig config = null) : base(title, config)
        {
        }

        /// <summary>
        /// Retrieves the string representation of the current configuration header.
        /// </summary>
        /// <returns>A string that represents the configuration header, or null if the configuration is not set.</returns>
        protected override string GetHeaderString()
        {
            return Config?.ToString();
        }

        /// <summary>
        /// Generates the complete Mermaid diagram as a formatted string.
        /// </summary>
        /// <returns>A string containing the full Mermaid diagram.</returns>
        public override string CalculateDiagram()
        {
            var lines = new List<string>();
            lines.Add(GetHeaderString());
            lines.Add(Name);

            lines.AddRange(Actions.Select(n => n.ToString()).Indent());

            return string.Join(Environment.NewLine, lines.ClearNewLines());
        }

        /// <summary>
        /// Creates a new branch in the Git graph with the specified name.
        /// </summary>
        /// <param name="branch">The name of the branch to create. Cannot be null, empty.</param>
        /// <returns>The current GitGraph instance, enabling method chaining.</returns>
        public GitGraph Branch(string branch)
        {
            if (string.IsNullOrWhiteSpace(branch))
                throw new ArgumentException("Branch name cannot be null or whitespace.", nameof(branch));

            _actions.Add(new GitBranch(branch));

            _branches.Add(branch);

            return this;
        }

        /// <summary>
        /// Switches the current working branch to the specified branch in the Git graph.
        /// </summary>
        /// <param name="branch">The name of the branch to switch to. Cannot be null, empty, or consist only of white-space characters.</param>
        /// <returns>The current GitGraph instance, enabling method chaining.</returns>
        public GitGraph Checkout(string branch)
        {
            if (string.IsNullOrWhiteSpace(branch))
                throw new ArgumentException("Branch name cannot be null or whitespace.", nameof(branch));

            if (Branches.All(b => b != branch) && branch != MainBranch)
                throw new InvalidOperationException($"Branch '{branch}' does not exist. Please create it first using Branch('{branch}').");

            _actions.Add(new GitCheckout(branch));
            return this;
        }

        /// <summary>
        /// Adds a commit to the graph with the specified identifier and an optional tag.
        /// </summary>
        /// <param name="id">The unique identifier for the commit to add.</param>
        /// <param name="tag">An optional tag to associate with the commit. If not specified, no tag is added.</param>
        /// <returns>The current instance of the graph, enabling method chaining.</returns>
        public GitGraph Commit(string id, string tag = "")
        {
            if (!string.IsNullOrWhiteSpace(id) && _commits.Contains(id))
                throw new InvalidOperationException($"Commit '{id}' already exists.");

            _actions.Add(new GitCommit(id, tag));

            if (!string.IsNullOrWhiteSpace(tag))
                _tags.Add(tag);
            return this;
        }

        /// <summary>
        /// Merges the specified branch into the current branch, optionally applying a tag to the merge operation.
        /// </summary>
        /// <param name="branch">The name of the branch to merge into the current branch. Must refer to an existing branch or the main
        /// branch.</param>
        /// <param name="tag">An optional tag to associate with the merge. If specified, the tag is added to the graph.</param>
        /// <returns>The current GitGraph instance, enabling method chaining.</returns>
        public GitGraph Merge(string branch, string tag = "")
        {
            if (string.IsNullOrWhiteSpace(branch))
                throw new ArgumentException("Branch name cannot be null or whitespace.", nameof(branch));

            if (Branches.All(b => b != branch) && branch != MainBranch)
                throw new InvalidOperationException($"Branch '{branch}' does not exist. Please create it first using Branch('{branch}').");

            _actions.Add(new GitMerge(branch, tag));

            if (!string.IsNullOrWhiteSpace(tag))
                _tags.Add(tag);
            return this;
        }
    }
}
