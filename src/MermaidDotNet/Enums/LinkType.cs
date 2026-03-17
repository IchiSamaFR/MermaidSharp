using MermaidDotNet.Attributes;
using System.ComponentModel;

namespace MermaidDotNet.Enums
{
    public enum LinkType
    {
        [MermaidEnum("--")]
        Normal,

        [MermaidEnum("-.", ".-")]
        Dotted,

        [MermaidEnum("==")]
        Thick,

        [MermaidEnum("~~~")]
        Invisible
    }
}
