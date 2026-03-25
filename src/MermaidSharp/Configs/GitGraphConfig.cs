using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System.Collections.Generic;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Represents the configuration settings for rendering a git graph diagram, including display options and main
    /// branch customization.
    /// </summary>
    public class GitGraphConfig : AConfig
    {
        private readonly string Name = "gitGraph";

        /// <summary>
        /// Gets or sets a value indicating whether the commit label is displayed.
        /// </summary>
        public bool? ShowCommitLabel { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether branches are displayed in the diagram.
        /// </summary>
        public bool? ShowBranches { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the commit label should be rotated in the diagram.
        /// </summary>
        public bool? RotateCommitLabel { get; set; }
        /// <summary>
        /// Gets or sets the name of the main branch in the repository.
        /// </summary>
        public string MainBranchName { get; set; }

        /// <summary>
        /// Initializes a new instance of the GitGraphConfig class with the specified theme and display options for the
        /// git graph.
        /// </summary>
        /// <param name="theme">The visual theme to apply to the git graph. The default is ConfigTheme.None.</param>
        /// <param name="showCommitLabel">A value indicating whether commit labels are displayed. If null, the default behavior is used.</param>
        /// <param name="showBranches">A value indicating whether branch lines are shown. If null, the default behavior is used.</param>
        /// <param name="rotateCommitLabel">A value indicating whether commit labels are rotated. If null, the default behavior is used.</param>
        /// <param name="mainBranchName">The name of the main branch to display. If null, the default branch name is used.</param>
        public GitGraphConfig(ConfigTheme theme = ConfigTheme.None,
            bool? showCommitLabel = null,
            bool? showBranches = null,
            bool? rotateCommitLabel = null,
            string mainBranchName = null) : base(theme)
        {
            ShowCommitLabel = showCommitLabel;
            ShowBranches = showBranches;
            RotateCommitLabel = rotateCommitLabel;
            MainBranchName = mainBranchName;
        }

        /// <summary>
        /// Retrieves a list of configuration parameters as formatted strings based on the current theme settings.
        /// </summary>
        /// <remarks>Override this method in a derived class to include additional configuration
        /// parameters as needed.</remarks>
        /// <returns>A list of strings representing the configuration parameters. The list is empty if no parameters are set.</returns>
        protected override List<string> GetParams()
        {
            var baseLst = base.GetParams();
            var lst = new List<string>();

            if (ShowCommitLabel != null)
                lst.Add($"showCommitLabel: {ShowCommitLabel.ToString().ToLower()}");

            if (ShowBranches != null)
                lst.Add($"showBranches: {ShowBranches.ToString().ToLower()}");

            if (RotateCommitLabel != null)
                lst.Add($"rotateCommitLabel: {RotateCommitLabel.ToString().ToLower()}");

            if (!string.IsNullOrWhiteSpace(MainBranchName))
                lst.Add($"mainBranchName: {MainBranchName}");

            if (lst.Count == 0)
                return baseLst;

            lst = lst.Indent();
            lst.Insert(0, $"{Name}:");
            baseLst.AddRange(lst.Indent());

            return baseLst;
        }
    }
}
