using MermaidDotNet.Enums;
using MermaidDotNet.Extensions;

namespace MermaidDotNet.Models
{
    public class FlowNode : Node
    {
        public FlowNode(string name, string text, ShapeType shape = ShapeType.Rectangle, string cssClass = "", string clickAction = "")
            : base(name, text, cssClass)
        {
            Shape = shape;
            ClickAction = clickAction;
        }

        public ShapeType Shape { get; set; }
        public string ClickAction { get; set; }

        public string ToClickString()
        {
            if (string.IsNullOrEmpty(ClickAction))
            {
                return string.Empty;
            }
            return $"click {Name} \"{ClickAction}\"";
        }

        protected override string GetSurroundedText()
        {
            return $"{Shape.StartString()}{Text}{Shape.EndString()}";
        }
    }
}
