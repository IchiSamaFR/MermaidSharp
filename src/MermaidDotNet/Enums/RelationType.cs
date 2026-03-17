using MermaidDotNet.Attributes;
using System.ComponentModel;

namespace MermaidDotNet.Enums
{
    public enum RelationType
    {
        [MermaidEnum("|o", "o|")]
        ZeroOrOne,

        [MermaidEnum("||")]
        ExactlyOne,

        [MermaidEnum("}o", "o{")]
        ZeroOrMore,

        [MermaidEnum("}|", "|{")]
        OneOrMore
    }
}