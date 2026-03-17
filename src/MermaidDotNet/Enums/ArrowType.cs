using MermaidDotNet.Attributes;

namespace MermaidDotNet.Enums
{
    public enum ArrowType
    {
        [MermaidEnum("<", ">")]
        Normal,

        [MermaidEnum("o")]
        Circle,

        [MermaidEnum("x")]
        Cross
    }
}
