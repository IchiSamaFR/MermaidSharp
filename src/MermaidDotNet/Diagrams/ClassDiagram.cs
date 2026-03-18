using MermaidDotNet.Extensions;
using MermaidDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidDotNet.Diagrams
{
    public class ClassDiagram : ADiagram
    {
        public override string Name => "classDiagram";

        public List<ClassNamespace> Namespaces { get; set; } = new List<ClassNamespace>();

        public ClassDiagram()
            : base()
        {
        }
        public ClassDiagram(string title)
            : base(title)
        {
        }

        public override string CalculateDiagram()
        {
            var lines = new List<string>();
            lines.Add(GetTitleString());
            lines.Add(Name);

            lines.AddRange(Namespaces.Select(n => n.ToString()).Indent());

            lines.AddRange(Nodes.Select(n => n.ToString()).Indent());
            lines.AddRange(Links.Select(n => n.ToString()).Indent());
            lines.AddRange(Nodes.Select(n => n.ToClassString()).Indent());

            return string.Join(Environment.NewLine, lines.ClearNewLines());
        }
    }
}
