using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;

namespace MermaidSharp.Samples.Examples
{
    /// <summary>
    /// Provides class diagram samples.
    /// </summary>
    public static class ClassDiagramExamples
    {
        public static IEnumerable<DiagramExample> Create()
        {
            yield return Hierarchy();
            yield return Relations();
            yield return GenericsAndNamespaces();
        }

        private static DiagramExample Hierarchy()
        {
            var diagram = new ClassDiagram("Animal hierarchy");
            diagram.Nodes.Add(new ClassNode("Animal",
                properties: new List<ClassProperty>
                {
                    new ClassProperty("Name", "string", ClassPropertyVisibility.Public),
                    new ClassProperty("Age", "int", ClassPropertyVisibility.Protected),
                    new ClassProperty("id", "Guid", ClassPropertyVisibility.Private)
                },
                methods: new List<ClassMethod>
                {
                    new ClassMethod("Speak", "string", ClassPropertyVisibility.Public),
                    new ClassMethod("Move", visibility: ClassPropertyVisibility.Protected,
                        parameters: new List<ClassMethodParam> { new ClassMethodParam("meters", "int") }),
                    new ClassMethod("Register", visibility: ClassPropertyVisibility.Internal)
                }));
            diagram.Nodes.Add(new ClassNode("Dog",
                properties: new List<ClassProperty> { new ClassProperty("Breed", "string", ClassPropertyVisibility.Public) },
                methods: new List<ClassMethod> { new ClassMethod("Fetch", "bool", ClassPropertyVisibility.Public) }));
            diagram.Nodes.Add(new ClassNode("Cat",
                methods: new List<ClassMethod> { new ClassMethod("Purr", visibility: ClassPropertyVisibility.Public) }));
            diagram.Links.Add(new ClassLink("Animal", "Dog", ClassLinkType.Inheritance));
            diagram.Links.Add(new ClassLink("Animal", "Cat", ClassLinkType.Inheritance));

            return new DiagramExample(DiagramKind.ClassDiagram, "class-hierarchy", "Inheritance, members and visibility",
                "Properties and methods with every visibility, parameters and return types.", diagram.CalculateDiagram());
        }

        private static DiagramExample Relations()
        {
            var diagram = new ClassDiagram("Relation types");
            var linkTypes = Enum.GetValues<ClassLinkType>();
            foreach (var linkType in linkTypes)
            {
                diagram.Nodes.Add(new ClassNode($"{linkType}Source"));
                diagram.Nodes.Add(new ClassNode($"{linkType}Target"));
                diagram.Links.Add(new ClassLink($"{linkType}Source", $"{linkType}Target", linkType, linkType.ToString()));
            }

            return new DiagramExample(DiagramKind.ClassDiagram, "class-relations", "Relation types",
                "Every ClassLinkType with its label.", diagram.CalculateDiagram());
        }

        private static DiagramExample GenericsAndNamespaces()
        {
            var diagram = new ClassDiagram("Repository pattern");
            diagram.Namespaces.Add(new ClassNamespace("Domain", new List<ClassNode>
            {
                new ClassNode("Order", properties: new List<ClassProperty>
                {
                    new ClassProperty("Id", "int", ClassPropertyVisibility.Public),
                    new ClassProperty("Lines", "List<OrderLine>", ClassPropertyVisibility.Public)
                }),
                new ClassNode("OrderLine")
            }));
            diagram.Namespaces.Add(new ClassNamespace("Infrastructure", new List<ClassNode>
            {
                new ClassNode("IRepository", "T", methods: new List<ClassMethod>
                {
                    new ClassMethod("GetById", "T", ClassPropertyVisibility.Public,
                        new List<ClassMethodParam> { new ClassMethodParam("id", "int") }),
                    new ClassMethod("GetAll", "IEnumerable<T>", ClassPropertyVisibility.Public)
                }),
                new ClassNode("OrderRepository")
            }));
            diagram.Links.Add(new ClassLink("IRepository", "OrderRepository", ClassLinkType.Realization));
            diagram.Links.Add(new ClassLink("OrderRepository", "Order", ClassLinkType.Dependency, "loads"));
            diagram.Links.Add(new ClassLink("Order", "OrderLine", ClassLinkType.Composition, "contains"));

            return new DiagramExample(DiagramKind.ClassDiagram, "class-generics-namespaces", "Generics and namespaces",
                "Generic types, generic members and namespaces.", diagram.CalculateDiagram());
        }
    }
}
