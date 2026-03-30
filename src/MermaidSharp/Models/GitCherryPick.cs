using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Models
{
    public class GitCherryPick : AGitAction
    {
        protected override string Name => "cherry-pick";

        public string CommitId { get; }

        public string Parent { get; }

        public string Tag { get; }

        public GitCherryPick(string commitId, string parent = "", string tag = "")
        {
            CommitId = commitId;
            Parent = parent;
            Tag = tag;
        }

        public override string ToString()
        {
            var returned = $"{Name} id:\"{CommitId}\"";

            if (!string.IsNullOrWhiteSpace(Parent))
                returned += $" parent: \"{Parent}\"";

            if (!string.IsNullOrWhiteSpace(Tag))
                returned += $" tag: \"{Tag}\"";

            return returned;
        }
    }
}
