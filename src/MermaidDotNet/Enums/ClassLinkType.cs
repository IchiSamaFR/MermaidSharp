using MermaidDotNet.Attributes;

namespace MermaidDotNet.Enums
{
    public enum ClassLinkType
    {
        [MermaidEnum("<|--")]
        Inheritance,

        [MermaidEnum("*--")]
        Composition,

        [MermaidEnum("o--")]
        Aggregation,

        [MermaidEnum("-->")]
        Association,

        [MermaidEnum("--")]
        SolidLink,

        [MermaidEnum("..>")]
        Dependency,

        [MermaidEnum("..|>")]
        Realization,

        [MermaidEnum("..")]
        DashedLink
    }
}