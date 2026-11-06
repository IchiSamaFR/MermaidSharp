using MermaidSharp.Configs;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;

namespace MermaidSharp.Samples.Examples
{
    /// <summary>
    /// Provides flowchart samples.
    /// </summary>
    public static class FlowchartExamples
    {
        public static IEnumerable<DiagramExample> Create()
        {
            yield return Shapes();
            yield return Links();
            yield return SubGraphs();
            yield return Styling();
            yield return SpecialCharacters();
        }

        private static DiagramExample Shapes()
        {
            var diagram = new FlowchartDiagram(FlowDirection.TopDown);
            FlowNode? previous = null;
            foreach (var shape in Enum.GetValues<FlowNodeShapeType>())
            {
                var node = new FlowNode($"shape{shape}", shape.ToString(), shape);
                diagram.Nodes.Add(node);
                if (previous != null)
                    diagram.Links.Add(new FlowLink(previous.Name, node.Name));
                previous = node;
            }

            return new DiagramExample(DiagramKind.Flowchart, "flowchart-shapes", "Node shapes",
                "Every FlowNodeShapeType, chained top-down.", diagram.CalculateDiagram());
        }

        private static DiagramExample Links()
        {
            var diagram = new FlowchartDiagram(FlowDirection.LeftRight);
            diagram.Nodes.AddRange(new[]
            {
                new FlowNode("a", "Normal"),
                new FlowNode("b", "Dotted"),
                new FlowNode("c", "Thick"),
                new FlowNode("d", "Circle arrow"),
                new FlowNode("e", "Cross arrow"),
                new FlowNode("f", "Bidirectional"),
                new FlowNode("g", "Invisible link")
            });
            diagram.Links.AddRange(new[]
            {
                new FlowLink("a", "b", "normal"),
                new FlowLink("b", "c", "dotted", linkType: FlowLinkType.Dotted),
                new FlowLink("c", "d", "thick", linkType: FlowLinkType.Thick),
                new FlowLink("d", "e", arrowType: FlowLinkArrowType.Circle),
                new FlowLink("e", "f", arrowType: FlowLinkArrowType.Cross),
                new FlowLink("f", "g", "both ways", isBidirectional: true),
                new FlowLink("g", "a", linkType: FlowLinkType.Invisible)
            });

            return new DiagramExample(DiagramKind.Flowchart, "flowchart-links", "Link types and arrows",
                "Normal, dotted, thick and invisible links, circle/cross arrows, labels and bidirectional links.",
                diagram.CalculateDiagram());
        }

        private static DiagramExample SubGraphs()
        {
            var diagram = new FlowchartDiagram("Order processing", FlowDirection.LeftRight);
            diagram.SubGraphs.Add(new FlowSubGraph("Frontend",
                new List<FlowNode> { new FlowNode("cart", "Cart"), new FlowNode("checkout", "Checkout") },
                new List<FlowLink> { new FlowLink("cart", "checkout") },
                FlowDirection.TopDown));
            diagram.SubGraphs.Add(new FlowSubGraph("Backend",
                new List<FlowNode>
                {
                    new FlowNode("api", "Orders API", FlowNodeShapeType.Subroutine),
                    new FlowNode("db", "Orders DB", FlowNodeShapeType.Cylinder)
                },
                new List<FlowLink> { new FlowLink("api", "db", "insert") }));
            diagram.Links.Add(new FlowLink("checkout", "api", "POST /orders"));

            return new DiagramExample(DiagramKind.Flowchart, "flowchart-subgraphs", "Subgraphs",
                "Subgraphs with their own direction, and links across subgraphs.", diagram.CalculateDiagram());
        }

        private static DiagramExample Styling()
        {
            var config = new FlowChartConfig(ConfigTheme.Forest, new FlowChartThemeVariables
            {
                PrimaryColor = "#e8f5e9",
                LineColor = "#2e7d32"
            });
            var diagram = new FlowchartDiagram("Styled flowchart", FlowDirection.TopDown, config);
            diagram.Nodes.AddRange(new[]
            {
                new FlowNode("start", "Start", FlowNodeShapeType.Stadium),
                new FlowNode("docs", "Open the docs", FlowNodeShapeType.Rounded, clickAction: "https://mermaid.js.org"),
                new FlowNode("finish", "Finish", FlowNodeShapeType.Stadium)
            });
            diagram.Links.AddRange(new[]
            {
                new FlowLink("start", "docs"),
                new FlowLink("docs", "finish", "done", "stroke:#2e7d32,stroke-width:3px", linkType: FlowLinkType.Thick)
            });

            return new DiagramExample(DiagramKind.Flowchart, "flowchart-styling", "Title, theme, link style and click",
                "Front matter title, Forest theme with theme variables, linkStyle and a clickable node.",
                diagram.CalculateDiagram());
        }

        private static DiagramExample SpecialCharacters()
        {
            var diagram = new FlowchartDiagram("Escaping: \"quotes\" & colons", FlowDirection.LeftRight);
            diagram.Nodes.AddRange(new[]
            {
                new FlowNode("input", "Read \"input\" (step 1)"),
                new FlowNode("check", "Valid? [yes/no]", FlowNodeShapeType.Rhombus),
                new FlowNode("output", "Write {result}")
            });
            diagram.Links.AddRange(new[]
            {
                new FlowLink("input", "check"),
                new FlowLink("check", "output", "ok (200)")
            });

            return new DiagramExample(DiagramKind.Flowchart, "flowchart-special-characters", "Special characters",
                "Labels and titles containing quotes, brackets and colons are escaped automatically.",
                diagram.CalculateDiagram());
        }
    }
}
