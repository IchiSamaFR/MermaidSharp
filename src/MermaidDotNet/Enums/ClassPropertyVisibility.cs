using MermaidDotNet.Attributes;

namespace MermaidDotNet.Enums
{
    public enum ClassPropertyVisibility
    {
        [MermaidEnum("")]
        None,

        [MermaidEnum("+")]
        Public,

        [MermaidEnum("-")]
        Private,

        [MermaidEnum("#")]
        Protected,

        [MermaidEnum("~")]
        Internal
    }
}
