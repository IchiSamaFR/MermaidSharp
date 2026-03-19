using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    /// <summary>
    /// Specifies the visibility level of a class property for use in Mermaid diagrams.
    /// </summary>
    /// <remarks>This enumeration is used to indicate the access modifier of a property when generating
    /// Mermaid class diagrams. Each value corresponds to a specific visibility symbol in Mermaid syntax.</remarks>
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
