using MermaidDotNet.Extensions;
using MermaidDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidDotNet.Diagrams
{
    /// <summary>
    /// Represents a Mermaid flowchart diagram, allowing configuration of direction, nodes, links, and subgraphs.
    /// </summary>
    /// <remarks>Use this class to construct and generate Mermaid-compatible flowchart diagrams with support
    /// for custom directions and subgraph grouping. The diagram can be rendered as a Mermaid syntax string for
    /// visualization or export. Only the directions "LR", "TD", "BT", "RL", and "TB" are supported. Attempting to use
    /// an unsupported direction will result in a NotSupportedException.</remarks>
    public class FlowchartDiagram : ADiagram
    {
        public override string Name => "flowchart";
        public string Direction { get; set; }
        public List<FlowSubGraph> SubGraphs { get; } = new List<FlowSubGraph>();

        /// <summary>
        /// Initializes a new instance of the FlowchartDiagram class with the specified title, direction, nodes, links,
        /// and subgraphs.
        /// </summary>
        /// <param name="direction">Accepts LR, TD, BT, RL, and TB options</param>
        public FlowchartDiagram(string direction) : this(string.Empty, direction)
        {

        }

        /// <summary>
        /// Initializes a new instance of the FlowchartDiagram class with the specified title, direction, nodes, links,
        /// and subgraphs.
        /// </summary>
        /// <param name="direction">Accepts LR, TD, BT, RL, and TB options</param>
        /// <param name="title">The title of the flowchart diagram</param>
        public FlowchartDiagram(string title, string direction) : base(title)
        {
            if (direction != "LR" && direction != "TD" && direction != "BT" && direction != "RL" && direction != "TB")
            {
                throw new NotSupportedException("Direction " + direction + " is currently unsupported");
            }
            Direction = direction;
        }

        /// <summary>
        /// Given a list of nodes and links, calculate the mermaid flowchart
        /// </summary>
        /// <returns>a mermaid graph as a string</returns>
        public override string CalculateDiagram()
        {
            var lines = new List<string>();
            lines.Add(GetTitleString());
            lines.Add($"{Name} {Direction}");

            lines.AddRange(SubGraphs.Select(sg => sg.ToString()).Indent());
            lines.AddRange(Nodes.Select(n => n.ToString()).Indent());
            lines.AddRange(Links.Select(n => n.ToString()).Indent());

            var allNodes = Nodes.OfType<FlowNode>()
                .Concat(SubGraphs.SelectMany(sg => sg.Nodes))
                .ToList();
            var allLinks = Links.OfType<FlowLink>()
                .Concat(SubGraphs.SelectMany(sg => sg.Links))
                .ToList();
            var linkStyles = allLinks.Where(l => !string.IsNullOrEmpty(l.LinkStyle)).ToList();
            lines.AddRange(linkStyles.Select(n => n.ToStyleString(linkStyles.IndexOf(n))).Indent());
            lines.AddRange(allNodes.Select(n => n.ToClassString()).Indent());
            lines.AddRange(allNodes.Select(n => n.ToClickString()).Indent());

            return string.Join(Environment.NewLine, lines.ClearNewLines());
        }
    }
}
