using MermaidSharp.Attributes;

namespace MermaidSharp.Enums
{
    public enum FlowDirection
    {
        [MermaidEnum]
        None,
        [MermaidEnum("TB")]
        TopBottom,
        [MermaidEnum("TD")]
        TopDown,
        [MermaidEnum("BT")]
        BottomTop,
        [MermaidEnum("RL")]
        RightLeft,
        [MermaidEnum("LR")]
        LeftRight
    }
}
