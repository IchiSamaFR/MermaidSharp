using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Models
{
    public class GitCheckout : AGitAction
    {
        public override string Name => "checkout";

        public string Branch { get; }

        public GitCheckout(string branch)
        {
            Branch = branch;
        }

        public override string ToString()
        {
            return $"{Name} {Branch}";
        }
    }
}
