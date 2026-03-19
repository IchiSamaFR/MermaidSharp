namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a directional link between two nodes in a flowchart diagram.
    /// </summary>
    /// <remarks>The Link class is used to define connections between nodes when generating Mermaid
    /// flowcharts. The link is directional, connecting a source node to a destination node. Derived classes can
    /// override the link style by overriding the GetLink method to produce different types of connections.</remarks>
    public abstract class ALink
    {
        public string SourceNode { get; set; }
        public string DestinationNode { get; set; }

        /// <summary>
        /// Initializes a new instance of the Link class that represents a connection between two nodes in a flowchart.
        /// </summary>
        /// <param name="sourceNode">The identifier of the source node for the link. Spaces will be removed from the value.</param>
        /// <param name="destinationNode">The identifier of the destination node for the link. Spaces will be removed from the value.</param>
        public ALink(string sourceNode, string destinationNode)
        {
            SourceNode = sourceNode.Replace(" ", "");
            DestinationNode = destinationNode.Replace(" ", "");
        }

        public override string ToString()
        {
            return $"{SourceNode}{GetLink()}{DestinationNode}";
        }
        protected virtual string GetLink()
        {
            return "-->";
        }
    }
}
