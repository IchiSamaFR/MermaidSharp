using MermaidSharp.Enums;
using MermaidSharp.Extensions;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a flowchart node with a specific shape and optional click action for use in Mermaid diagrams.
    /// </summary>
    /// <remarks>A FlowNode extends the base Node class by allowing the specification of a node shape and an
    /// optional click action. The click action can be used to define interactive behavior in rendered Mermaid diagrams,
    /// such as navigation or custom JavaScript execution. This class is typically used when constructing flowcharts
    /// that require nodes with distinct shapes or interactive capabilities.</remarks>
    public class FlowNode : ANode
    {
        public FlowNodeShapeType Shape { get; set; }
        public string ClickAction { get; set; }

        /// <summary>
        /// Initializes a new instance of the FlowNode class with the specified name, display text, shape, CSS class,
        /// and click action.
        /// </summary>
        /// <param name="name">The unique identifier for the node within the flowchart.</param>
        /// <param name="text">The display text shown inside the node.</param>
        /// <param name="shape">The shape of the node. Defaults to ShapeType.Rectangle.</param>
        /// <param name="cssClass">An optional CSS class to apply custom styling to the node. If not specified, no additional CSS class is
        /// applied.</param>
        /// <param name="clickAction">An optional action or URL to be triggered when the node is clicked. If not specified, the node will not have
        /// a click action.</param>
        public FlowNode(string name, string text, FlowNodeShapeType shape = FlowNodeShapeType.Rectangle, string cssClass = "", string clickAction = "")
            : base(name, text, cssClass)
        {
            Shape = shape;
            ClickAction = clickAction;
        }

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
