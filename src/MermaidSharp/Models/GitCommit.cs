using MermaidSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Models
{
    public class GitCommit : AGitAction
    {
        public override string Name => "commit";

        public string Id { get; }
        public string Tag { get; }

        public GitCommit(string id = "", string tag = "")
        {
            Id = id;
            Tag = tag;
        }

        public override string ToString()
        {
            var returned = Name;
            if (!string.IsNullOrWhiteSpace(Id))
                returned += $" id: \"{Id}\"";
            if (!string.IsNullOrWhiteSpace(Tag))
                returned += $" tag: \"{Tag}\"";
            return returned;
        }
    }
}
