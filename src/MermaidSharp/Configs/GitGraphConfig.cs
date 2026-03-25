using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MermaidSharp.Configs
{
    public class GitGraphConfig : AConfig
    {
        private readonly string Name = "gitGraph";

        public bool? ShowCommitLabel { get; set; }
        public bool? ShowBranches { get; set; }
        public bool? RotateCommitLabel { get; set; }
        public string MainBranchName { get; set; }

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

            if(lst.Count == 0)
                return baseLst;

            lst = lst.Indent();
            lst.Insert(0, $"{Name}:");
            baseLst.AddRange(lst.Indent());

            return baseLst;
        }
    }
}
