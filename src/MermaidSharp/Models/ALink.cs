namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a directional link between two nodes in a flowchart diagram.
    /// </summary>
    public abstract class ALink
    {
        public string SourceNode { get; set; }
        public string DestinationNode { get; set; }

        /// <summary>
        /// Initializes a new instance of the Link class that represents a connection between two nodes in a flowchart.
        /// </summary>
        /// <param name="sourceNode">The identifier of the source node for the link. Spaces will be removed from the value.</param>
        /// <param name="destinationNode">The identifier of the destination node for the link. Spaces will be removed from the value.</param>
        protected ALink(string sourceNode, string destinationNode)
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
