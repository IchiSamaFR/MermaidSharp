using MermaidDotNet.Constants;
using MermaidDotNet.Extensions;
using MermaidDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidDotNet.Diagrams
{
    /// <summary>
    /// Represents the base class for diagram types that consist of nodes and links, providing common functionality for
    /// diagram generation.
    /// </summary>
    /// <remarks>This abstract class defines the structure and core behavior for diagrams such as flowcharts.
    /// Derived classes should specify the diagram type by implementing the Name property and may override diagram
    /// calculation logic as needed. The Nodes and Links collections define the elements and relationships within the
    /// diagram.</remarks>
    public abstract class ADiagram
    {
        public abstract string Name { get; }
        public string Title { get; set; }
        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<Link> Links { get; set; } = new List<Link>();

        public ADiagram()
        {
        }
        public ADiagram(string title)
        {
            Title = title;
        }

        /// <summary>
        /// Given a list of nodes and links, calculate the mermaid flowchart
        /// </summary>
        /// <returns>a mermaid graph as a string</returns>
        public virtual string CalculateDiagram()
        {
            var lines = new List<string>();
            lines.Add(GetTitleString());
            lines.Add(Name);

            lines.AddRange(Nodes.Select(n => n.ToString()).Indent());
            lines.AddRange(Links.Select(n => n.ToString()).Indent());
            lines.AddRange(Nodes.Select(n => n.ToClassString()).Indent());

            return string.Join(Environment.NewLine, lines.ClearNewLines());
        }

        protected string GetTitleString()
        {
            if (string.IsNullOrEmpty(Title))
            {
                return string.Empty;
            }
            var lines = new List<string>();
            lines.Add(FormattingConstants.TitleSeparator);
            lines.Add($"title: {Title}");
            lines.Add(FormattingConstants.TitleSeparator);
            return string.Join(Environment.NewLine, lines);
        }
    }
}
