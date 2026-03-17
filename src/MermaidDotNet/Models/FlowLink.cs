using MermaidDotNet.Enums;
using MermaidDotNet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidDotNet.Models
{
    public class FlowLink : Link
    {
        public FlowLink(string sourceNode, string destinationNode, string label = "", string linkstyle = "", bool isBidirectional = false, LinkType linkType = LinkType.Normal, ArrowType arrowType = ArrowType.Normal)
            : base(sourceNode, destinationNode)
        {
            Label = label;
            IsBidirectional = isBidirectional;
            LinkStyle = linkstyle;
            Type = linkType;
            Arrow = arrowType;
        }

        public string Label { get; set; }
        public string LinkStyle { get; set; }
        public bool IsBidirectional { get; }
        public LinkType Type { get; set; }
        public ArrowType Arrow { get; set; }

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
