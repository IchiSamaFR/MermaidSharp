using MermaidDotNet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidDotNet.Models
{
    public class ClassNode : Node
    {
        public string Type { get; set; }
        public List<ClassProperty> Properties { get;}
        public List<ClassMethod> Methods { get; }

        public ClassNode(string name, string type = "", string text = "", List<ClassProperty> properties = null, List<ClassMethod> methods = null) : base(name, text, string.Empty)
        {
            Type = type;
            Properties = properties ?? new List<ClassProperty>();
            Methods = methods ?? new List<ClassMethod>();
        }

        public override string ToString()
        {
            var lines = new List<string>();
            var nameString = new string[] {
                "class",
                Name,
                string.IsNullOrEmpty(Type) ? string.Empty : $"~{Type.FormatAngleBracket()}~"
            }.JoinNonEmpty(" ");

            if (Properties.Count == 0 && Methods.Count == 0)
            {
                return nameString;
            }
            lines.Add(string.Join(" ", nameString, "{"));
            lines.AddRange(Properties.Select(p => p.ToString()).Indent());
            lines.AddRange(Methods.Select(m => m.ToString()).Indent());
            lines.Add("}");
            return string.Join(Environment.NewLine, lines);
        }
    }
}
