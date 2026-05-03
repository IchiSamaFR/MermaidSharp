using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the possible positions for the Y axis in a Mermaid quadrant chart.
    /// </summary>
    public enum QuadrantYAxisPosition
    {
        /// <summary>
        /// The Y axis is positioned on the left.
        /// </summary>
        [MermaidEnum("left")]
        Left,

        /// <summary>
        /// The Y axis is positioned on the right.
        /// </summary>
        [MermaidEnum("right")]
        Right
    }
}