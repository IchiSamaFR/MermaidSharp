using MermaidSharp.Extensions;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Diagrams
{
    /// <summary>
    /// Represents the base class for diagram types that consist of nodes and links, providing common functionality for
    /// diagram generation.
    /// </summary>
    public abstract class ADiagram : AMermaid
    {
        public List<ANode> Nodes { get; } = new List<ANode>();
        public List<ALink> Links { get; } = new List<ALink>();

        /// <summary>
        /// Initializes a new instance of the ADiagram class with the specified title.
        /// </summary>
        /// <param name="title">The title to assign to the diagram. If not specified, the title is set to an empty string.</param>
        protected ADiagram(string title = "")
        {
            Title = title;
        }

        /// <summary>
        /// Given a list of nodes and links, calculate the mermaid flowchart
        /// </summary>
        /// <returns>a mermaid graph as a string</returns>
        public override string CalculateDiagram()
        {
            var lines = new List<string>();
            lines.Add(GetHeaderString());
            lines.Add(Name);

            lines.AddRange(Nodes.Select(n => n.ToString()).Indent());
            lines.AddRange(Links.Select(n => n.ToString()).Indent());
            lines.AddRange(Nodes.Select(n => n.ToClassString()).Indent());

            return string.Join(Environment.NewLine, lines.ClearNewLines());
        }
    }
}
