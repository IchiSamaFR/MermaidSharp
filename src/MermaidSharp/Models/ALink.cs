using MermaidSharp.Extensions;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a directional link between two nodes in a flowchart diagram.
    /// </summary>
    public abstract class ALink
    {
        private string _sourceNode;
        private string _destinationNode;

        /// <summary>
        /// Gets or sets the identifier of the source node in the diagram.
        /// </summary>
        public string SourceNode
        {
            get => _sourceNode;
            set => _sourceNode = value.ToMermaidId(nameof(SourceNode));
        }

        /// <summary>
        /// Gets or sets the identifier of the destination node.
        /// </summary>
        public string DestinationNode
        {
            get => _destinationNode;
            set => _destinationNode = value.ToMermaidId(nameof(DestinationNode));
        }

        /// <summary>
        /// Initializes a new instance of the Link class that represents a connection between two nodes in a flowchart.
        /// </summary>
        /// <param name="sourceNode">The identifier of the source node for the link. Spaces will be removed from the value.</param>
        /// <param name="destinationNode">The identifier of the destination node for the link. Spaces will be removed from the value.</param>
        protected ALink(string sourceNode, string destinationNode)
        {
            _sourceNode = sourceNode.ToMermaidId(nameof(sourceNode));
            _destinationNode = destinationNode.ToMermaidId(nameof(destinationNode));
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            return $"{SourceNode}{GetLink()}{DestinationNode}";
        }

        /// <summary>
        /// Returns the default link representation used in Mermaid diagrams.
        /// </summary>
        /// <returns>A string that represents the default link symbol.</returns>
        protected virtual string GetLink()
        {
            return "-->";
        }
    }
}
