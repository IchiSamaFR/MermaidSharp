using MermaidDotNet.Constants;
using MermaidDotNet.Extensions;
using MermaidDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidDotNet.Diagrams
{
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

            lines.AddRange(SubGraphs.Select(sg => sg.GetSubGraphString()).ClearNewLines().Indent());
            lines.AddRange(Nodes.Select(n => n.GetNodeString()).ClearNewLines().Indent());
            lines.AddRange(Links.Select(n => n.GetLinkString()).ClearNewLines().Indent());

            var allNodes = Nodes.OfType<FlowNode>()
                .Concat(SubGraphs.SelectMany(sg => sg.Nodes))
                .ToList();
            var allLinks = Links.OfType<FlowLink>()
                .Concat(SubGraphs.SelectMany(sg => sg.Links))
                .ToList();
            var linkStyles = allLinks.Where(l => !string.IsNullOrEmpty(l.LinkStyle)).ToList();
            lines.AddRange(linkStyles.Select(n => n.GetStyleString(linkStyles.IndexOf(n))).ClearNewLines().Indent());
            lines.AddRange(allNodes.Select(n => n.GetClassString()).ClearNewLines().Indent());
            lines.AddRange(allNodes.Select(n => n.GetClickActionString()).ClearNewLines().Indent());

            return string.Join(Environment.NewLine, lines);
        }
    }
}
