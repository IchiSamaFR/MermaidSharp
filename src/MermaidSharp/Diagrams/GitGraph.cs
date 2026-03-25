using MermaidSharp.Configs;
using MermaidSharp.Extensions;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MermaidSharp.Diagrams
{
    public class GitGraph : AGraph
    {
        public override string Name => "gitGraph";
        public new GitGraphConfig Config => base.Config as GitGraphConfig;

        private List<AGitAction> _actions = new List<AGitAction>();
        private List<string> _branches = new List<string>();
        private List<string> _tags = new List<string>();

        public string MainBranch => Config?.MainBranchName ?? "main";
        public IReadOnlyList<AGitAction> Actions => _actions;
        public IReadOnlyList<string> Branches => _branches;
        public IReadOnlyList<string> Tags => _tags;

        public GitGraph(string title = "", GitGraphConfig config = null) : base(title, config)
        {
        }

        protected override string GetHeaderString()
        {
            return Config?.ToString();
        }
        public override string CalculateDiagram()
        {
            var lines = new List<string>();
            lines.Add(GetHeaderString());
            lines.Add(Name);

            lines.AddRange(Actions.Select(n => n.ToString()).Indent());

            return string.Join(Environment.NewLine, lines.ClearNewLines());
        }

        public GitGraph Branch(string branch)
        {
            if (string.IsNullOrWhiteSpace(branch))
                throw new ArgumentException("Branch name cannot be null or whitespace.", nameof(branch));

            _actions.Add(new GitBranch(branch));

            _branches.Add(branch);

            return this;
        }

        public GitGraph Checkout(string branch)
        {
            if (string.IsNullOrWhiteSpace(branch))
                throw new ArgumentException("Branch name cannot be null or whitespace.", nameof(branch));

            if (Branches.All(b => b != branch) && branch != MainBranch)
                throw new InvalidOperationException($"Branch '{branch}' does not exist. Please create it first using Branch('{branch}').");

            _actions.Add(new GitCheckout(branch));
            return this;
        }

        public GitGraph Commit(string id, string tag = "")
        {
            _actions.Add(new GitCommit(id, tag));

            if (!string.IsNullOrWhiteSpace(tag))
                _tags.Add(tag);
            return this;
        }

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
