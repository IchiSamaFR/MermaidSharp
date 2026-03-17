using MermaidDotNet.Constants;
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
        public List<FlowSubGraph> SubGraphs { get; set; } = new List<FlowSubGraph>();

        /// <summary>
        /// Initialize the flowchart
        /// </summary>
        /// <param name="direction">Accepts LR, TD, BT, RL, and TB options</param>
        /// <param name="nodes">A list of nodes</param>
        /// <param name="links">A list of links</param>
        public FlowchartDiagram(string direction, List<FlowNode> nodes, List<FlowLink> links)
                : this(direction, nodes, links, new List<FlowSubGraph>())
        {
        }

        /// <summary>
        /// Initialize the flowchart
        /// </summary>
        /// <param name="direction">Accepts LR, TD, BT, RL, and TB options</param>
        /// <param name="nodes">A list of nodes</param>
        /// <param name="links">A list of links</param>
        /// <param name="subGraphs">A list of subgraphs</param>
        public FlowchartDiagram(string direction, List<FlowNode> nodes, List<FlowLink> links, List<FlowSubGraph> subGraphs)
                : base(nodes.Cast<Node>().ToList(), links.Cast<Link>().ToList())
        {
            if (direction != "LR" && direction != "TD" && direction != "BT" && direction != "RL" && direction != "TB")
            {
                throw new NotSupportedException("Direction " + direction + " is currently unsupported");
            }

            Direction = direction;
            SubGraphs = subGraphs;
        }

        /// <summary>
        /// Given a list of nodes and links, calculate the mermaid flowchart
        /// </summary>
        /// <returns>a mermaid graph as a string</returns>
        public override string CalculateDiagram()
        {
            var lines = new List<string>();
            lines.Add($"{Name} {Direction}");

            lines.AddRange(SubGraphs.Select(sg => sg.ToString()).ClearNewLines().Indent());
            lines.AddRange(Nodes.Select(n => n.ToString()).ClearNewLines().Indent());
            lines.AddRange(Links.Select(n => n.ToString()).ClearNewLines().Indent());

            var allNodes = Nodes.OfType<FlowNode>()
                .Concat(SubGraphs.SelectMany(sg => sg.Nodes))
                .ToList();
            var allLinks = Links.OfType<FlowLink>()
                .Concat(SubGraphs.SelectMany(sg => sg.Links))
                .ToList();
            var linkStyles = allLinks.Where(l => !string.IsNullOrEmpty(l.LinkStyle)).ToList();
            lines.AddRange(linkStyles.Select(n => n.ToStyleString(linkStyles.IndexOf(n))).ClearNewLines().Indent());
            lines.AddRange(allNodes.Select(n => n.ToClassString()).ClearNewLines().Indent());
            lines.AddRange(allNodes.Select(n => n.ToClickString()).ClearNewLines().Indent());

            return string.Join(Environment.NewLine, lines);
        }
    }
}
