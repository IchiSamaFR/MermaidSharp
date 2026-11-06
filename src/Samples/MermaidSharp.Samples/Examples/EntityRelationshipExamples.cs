using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;

namespace MermaidSharp.Samples.Examples
{
    /// <summary>
    /// Provides entity relationship diagram samples.
    /// </summary>
    public static class EntityRelationshipExamples
    {
        public static IEnumerable<DiagramExample> Create()
        {
            yield return Shop();
            yield return Cardinalities();
        }

        private static DiagramExample Shop()
        {
            var diagram = new EntityRelationshipDiagram("Online shop");
            diagram.Nodes.AddRange(new[]
            {
                new EntityRelationNode("Customer", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("CustomerId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("Email", "string", RelationConstraintType.UniqueKey, "Login of the customer"),
                    new EntityRelationColumn("Name", "string")
                }),
                new EntityRelationNode("Order", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("OrderId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("CustomerId", "int", RelationConstraintType.ForeignKey),
                    new EntityRelationColumn("CreatedAt", "datetime")
                }),
                new EntityRelationNode("OrderLine", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("OrderId", "int", RelationConstraintType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("ProductId", "int", RelationConstraintType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("Quantity", "int", comment: "Must be greater than 0")
                }),
                new EntityRelationNode("Product", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("ProductId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("Sku", "string", RelationConstraintType.UniqueKey),
                    new EntityRelationColumn("Price", "decimal")
                })
            });
            diagram.Links.AddRange(new[]
            {
                new EntityRelationLink("Customer", "Order", RelationLinkType.ExactlyOne, RelationLinkType.ZeroOrMore, "places"),
                new EntityRelationLink("Order", "OrderLine", RelationLinkType.ExactlyOne, RelationLinkType.OneOrMore, "contains"),
                new EntityRelationLink("Product", "OrderLine", RelationLinkType.ExactlyOne, RelationLinkType.ZeroOrMore, "is ordered in")
            });

            return new DiagramExample(DiagramKind.EntityRelationship, "er-shop", "Online shop",
                "Primary, foreign, unique and composite keys, column comments and labelled relations.",
                diagram.CalculateDiagram());
        }

        private static DiagramExample Cardinalities()
        {
            var diagram = new EntityRelationshipDiagram("Cardinalities");
            foreach (var cardinality in Enum.GetValues<RelationLinkType>())
            {
                diagram.Nodes.Add(new EntityRelationNode($"{cardinality}Parent"));
                diagram.Nodes.Add(new EntityRelationNode($"{cardinality}Child"));
                diagram.Links.Add(new EntityRelationLink($"{cardinality}Parent", $"{cardinality}Child",
                    RelationLinkType.ExactlyOne, cardinality, cardinality.ToString()));
            }

            return new DiagramExample(DiagramKind.EntityRelationship, "er-cardinalities", "Cardinalities",
                "Every RelationLinkType on the child side of a relation.", diagram.CalculateDiagram());
        }
    }
}
