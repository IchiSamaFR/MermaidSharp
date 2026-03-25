using MermaidSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Models
{
    public class GitBranch : AGitAction
    {
        public override string Name => "branch";

        public string Branch { get; }

        public GitBranch(string branch)
        {
            Branch = branch;
        }

        public override string ToString()
        {
            return $"{Name} {Branch}";
        }
    }
}
