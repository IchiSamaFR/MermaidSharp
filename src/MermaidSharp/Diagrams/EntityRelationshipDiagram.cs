namespace MermaidSharp.Diagrams
{
    /// <summary>
    /// Represents an entity-relationship diagram that models entities and their relationships using Mermaid syntax.
    /// </summary>
    /// <remarks>Use this class to define and generate entity-relationship diagrams for visualization or
    /// documentation purposes. The diagram is constructed from a collection of entity nodes and relationship links, and
    /// supports Mermaid's ER diagram features.</remarks>
    public class EntityRelationshipDiagram : ADiagram
    {
        public override string Name => "erDiagram";

        /// <summary>
        /// Initializes a new instance of the EntityRelationshipDiagram class with the specified title.
        /// </summary>
        /// <param name="title">The title of the entity-relationship diagram. If not specified, the diagram will have an empty title.</param>
        public EntityRelationshipDiagram(string title = "") : base(title)
        {
        }
    }
}
