using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Models
{
    public class GitMerge : AGitAction
    {
        public override string Name => "merge";

        public string Branch { get; }
        public string Tag { get; }

        public GitMerge(string branch, string tag = "")
        {
            Branch = branch;
            Tag = tag;
        }

        public override string ToString()
        {
            var returned = $"{Name} {Branch}";
            if (!string.IsNullOrWhiteSpace(Tag))
                returned += $" tag: \"{Tag}\"";
            return returned;
        }
    }
}
