using MermaidDotNet.Models;
using System.Collections.Generic;
using System.Linq;

namespace MermaidDotNet.Diagrams
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

        public EntityRelationshipDiagram() : base()
        {
        }
        public EntityRelationshipDiagram(string title) : base(title)
        {
        }
    }
}
