using MermaidSharp.Enums;
using MermaidSharp.Extensions;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a directional link between two entities with specified relation types at the source and destination
    /// nodes, and an optional label.
    /// </summary>
    /// <remarks>Use this class to model relationships between entities where the type of relation at each end
    /// may differ, such as in entity-relationship diagrams. The label can be used to annotate the link with additional
    /// information.</remarks>
    public class EntityRelationLink : ALink
    {
        public string Label { get; }
        public RelationLinkType SourceRelation { get; }
        public RelationLinkType DestinationRelation { get; }

        /// <summary>
        /// Initializes a new instance of the EntityRelationLink class, representing a relationship between two nodes
        /// with specified relation types and an optional label.
        /// </summary>
        /// <param name="sourceNode">The identifier of the source node in the relationship.</param>
        /// <param name="destinationNode">The identifier of the destination node in the relationship.</param>
        /// <param name="sourceRelation">The relation type from the source node.</param>
        /// <param name="destinationRelation">The relation type from the destination node.</param>
        /// <param name="label">An optional label describing the relationship. The default is an empty string.</param>
        public EntityRelationLink(
            string sourceNode,
            string destinationNode,
            RelationLinkType sourceRelation,
            RelationLinkType destinationRelation,
            string label = "")
            : base(
                sourceNode,
                destinationNode)
        {
            SourceRelation = sourceRelation;
            DestinationRelation = destinationRelation;
            Label = label;
        }

        public override string ToString()
        {
            string label = !string.IsNullOrEmpty(Label) ? $": \"{Label}\"" : string.Empty;

            var returnedParts = new string[]
            {
                SourceNode,
                $"{SourceRelation.StartString()}--{DestinationRelation.EndString()}",
                DestinationNode,
                label
            };

            return returnedParts.JoinNonEmpty(" ");
        }
    }
}
