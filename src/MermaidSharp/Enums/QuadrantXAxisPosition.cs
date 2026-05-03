using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the possible positions for the X axis in a Mermaid quadrant chart.
    /// </summary>
    public enum QuadrantXAxisPosition
    {
        /// <summary>
        /// The X axis is positioned at the top.
        /// </summary>
        [MermaidEnum("top")]
        Top,

        /// <summary>
        /// The X axis is positioned at the bottom.
        /// </summary>
        [MermaidEnum("bottom")]
        Bottom
    }
}