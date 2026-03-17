using MermaidDotNet.Attributes;
using System.ComponentModel;

namespace MermaidDotNet.Enums
{
    public enum ShapeType
    {
        [MermaidEnum("[", "]")]
        Rectangle,

        [MermaidEnum("(", ")")]
        Rounded,

        [MermaidEnum("([", "])")]
        Stadium,

        [MermaidEnum("[(", ")]")]
        Cylinder,

        [MermaidEnum("((", "))")]
        Circle,

        [MermaidEnum("{", "}")]
        Rhombus,

        [MermaidEnum("{{", "}}")]
        Hexagon,

        [MermaidEnum("[/", "/]")]
        Parallelogram,

        [MermaidEnum("[\\", "\\]")]
        Trapezoid,

        [MermaidEnum("[/", "\\]")]
        TrapezoidAlt,

        [MermaidEnum("[[", "]]")]
        Subroutine
    }
}