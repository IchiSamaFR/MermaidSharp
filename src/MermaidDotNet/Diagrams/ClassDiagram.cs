using MermaidDotNet.Extensions;
using MermaidDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidDotNet.Diagrams
{
    /// <summary>
    /// Represents a Mermaid class diagram, including namespaces, classes, and relationships.
    /// </summary>
    /// <remarks>Use this class to construct and generate Mermaid class diagrams programmatically. The diagram
    /// can include multiple namespaces, classes, and links between them. Inherit from this class to extend or customize
    /// class diagram generation.</remarks>
    public class ClassDiagram : ADiagram
    {
        public override string Name => "classDiagram";

        public List<ClassNamespace> Namespaces { get; set; } = new List<ClassNamespace>();

        /// <summary>
        /// Initializes a new instance of the ClassDiagram class with the specified title.
        /// </summary>
        /// <param name="title">The title of the class diagram. If not specified, the title is set to an empty string.</param>
        public ClassDiagram(string title = "")
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
