using MermaidSharp.Enums;
using MermaidSharp.Extensions;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a relationship between two classes in a Mermaid class diagram, including the type of link and an
    /// optional label.
    /// </summary>
    /// <remarks>Use this class to define associations, dependencies, or other relationships between classes
    /// when generating Mermaid class diagrams. The link type determines the visual representation of the relationship,
    /// while the label provides additional context or description for the link.</remarks>
    public class ClassLink : ALink
    {
        public string Label { get; set; }
        public ClassLinkType LinkType { get; set; }

        /// <summary>
        /// Initializes a new instance of the ClassLink class with the specified source, target, link type, and optional
        /// label.
        /// </summary>
        /// <param name="source">The identifier of the source class in the relationship.</param>
        /// <param name="target">The identifier of the target class in the relationship.</param>
        /// <param name="linkType">The type of link representing the relationship between the source and target classes.</param>
        /// <param name="label">An optional label describing the relationship. The default is an empty string.</param>
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
