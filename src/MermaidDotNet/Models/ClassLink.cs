using MermaidDotNet.Enums;
using MermaidDotNet.Extensions;

namespace MermaidDotNet.Models
{
    public class ClassLink : Link
    {
        public string Label { get; set; }
        public ClassLinkType LinkType { get; set; }

        public ClassLink(string source, string target, ClassLinkType linkType, string label = "") : base(source, target)
        {
            LinkType = linkType;
            Label = label;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Label))
            {
                return $"{SourceNode}{GetLink()}{DestinationNode}";
            }
            return $"{SourceNode}{GetLink()}{DestinationNode} : {Label}";
        }

        protected override string GetLink()
        {
            return LinkType.StartString();
        }
    }
}
