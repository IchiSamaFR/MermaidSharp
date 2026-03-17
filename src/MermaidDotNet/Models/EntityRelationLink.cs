using MermaidDotNet.Enums;
using MermaidDotNet.Extensions;
using System.Xml.Linq;

namespace MermaidDotNet.Models
{
    public class EntityRelationLink : Link
    {
        public string Label { get; }
        public RelationType SourceRelation { get; }
        public RelationType DestinationRelation { get; }

        public EntityRelationLink(
            string sourceNode,
            string destinationNode,
            string label,
            RelationType sourceRelation,
            RelationType destinationRelation)
            : base(
                sourceNode,
                destinationNode)
        {
            Label = label;
            SourceRelation = sourceRelation;
            DestinationRelation = destinationRelation;
        }

        public EntityRelationLink(
            string sourceNode,
            string destinationNode,
            RelationType sourceRelation,
            RelationType destinationRelation)
            : this(
                sourceNode,
                destinationNode,
                null,
                sourceRelation,
                destinationRelation)
        {
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