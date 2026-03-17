using MermaidDotNet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidDotNet.Models
{
    public class ClassNamespace
    {
        public string Name { get; set; }
        public List<ClassNode> Classes { get; }

        public ClassNamespace(string name, List<ClassNode> classes = null)
        {
            Name = name;
            Classes = classes ?? new List<ClassNode>();
        }

        public override string ToString()
        {
            var lines = new List<string>();
            var nameString = new string[] {
                "namespace",
                Name
            }.JoinNonEmpty(" ");
            lines.Add(string.Join(" ", nameString, "{"));
            lines.AddRange(Classes.Select(c => c.ToString()).Indent());
            lines.Add("}");
            return string.Join(Environment.NewLine, lines);
        }
    }
}
