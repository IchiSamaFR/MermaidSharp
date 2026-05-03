using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the possible positions for the Y axis in a Mermaid quadrant chart.
    /// </summary>
    public enum QuadrantYAxisPosition
    {
#pragma warning disable CS1591
        [MermaidEnum("left")]
        Left,

        [MermaidEnum("right")]
        Right
#pragma warning restore CS1591
    }
}