using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System.Text;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a directional or bidirectional link between two nodes in a Mermaid flowchart, including label, style,
    /// and arrow customization options.
    /// </summary>
    /// <remarks>Use this class to define advanced flowchart connections with custom labels, styles, and arrow
    /// types. The link can be configured as bidirectional and supports various visual styles for integration with
    /// Mermaid diagrams.</remarks>
    public class FlowLink : ALink
    {
        public string Label { get; set; }
        public string LinkStyle { get; set; }
        public bool IsBidirectional { get; }
        public FlowLinkType Type { get; set; }
        public FlowLinkArrowType Arrow { get; set; }

        /// <summary>
        /// Initializes a new instance of the FlowLink class, representing a connection between two nodes in a flowchart
        /// with optional labeling, styling, directionality, and link or arrow types.
        /// </summary>
        /// <param name="sourceNode">The identifier of the source node from which the link originates. Cannot be null or empty.</param>
        /// <param name="destinationNode">The identifier of the destination node to which the link points. Cannot be null or empty.</param>
        /// <param name="label">The label to display on the link. If not specified, the link will have no label.</param>
        /// <param name="linkstyle">The style to apply to the link, such as line type or color. If not specified, the default style is used.</param>
        /// <param name="isBidirectional">true if the link should be bidirectional; otherwise, false.</param>
        /// <param name="linkType">The type of link to use, such as normal or dotted. Specifies the visual representation of the link.</param>
        /// <param name="arrowType">The type of arrow to use at the end of the link, such as normal or open.</param>
        public FlowLink(string sourceNode, string destinationNode, string label = "", string linkstyle = "", bool isBidirectional = false, FlowLinkType linkType = FlowLinkType.Normal, FlowLinkArrowType arrowType = FlowLinkArrowType.Normal)
            : base(sourceNode, destinationNode)
        {
            Label = label;
            IsBidirectional = isBidirectional;
            LinkStyle = linkstyle;
            Type = linkType;
            Arrow = arrowType;
        }

        public string ToStyleString(int index)
        {
            if (string.IsNullOrEmpty(LinkStyle))
            {
                return string.Empty;
            }
            return $"linkStyle {index} {LinkStyle}";
        }

        protected override string GetLink()
        {
            StringBuilder sb = new StringBuilder();
            if (IsBidirectional)
            {
                sb.Append(Arrow.StartString());
            }
            sb.Append(Type.StartString());
            if (!string.IsNullOrEmpty(Label))
            {
                sb.Append(Label);
                sb.Append(Type.EndString());
            }
            sb.Append(Arrow.EndString());
            return sb.ToString();
        }
    }
}
