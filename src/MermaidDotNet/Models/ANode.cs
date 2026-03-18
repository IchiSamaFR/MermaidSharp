using System.Text;

namespace MermaidDotNet.Models
{
    /// <summary>
    /// Represents a node in a Mermaid flowchart, containing a unique name, display text, and optional CSS class for
    /// styling.
    /// </summary>
    /// <remarks>A node defines an individual element within a Mermaid diagram, such as a process, decision,
    /// or state. The node's name must be unique within the flowchart and is used as its identifier in links and
    /// references. The display text is shown inside the node shape. The optional CSS class allows for custom styling of
    /// the node when rendered.</remarks>
    public class ANode
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string CssClass { get; set; }

        /// <summary>
        /// Initializes a new instance of the Node class with the specified name, display text, and optional CSS class.
        /// </summary>
        /// <param name="name">The unique identifier for the node. Spaces will be removed from this value.</param>
        /// <param name="text">The display text associated with the node.</param>
        /// <param name="cssClass">An optional CSS class to apply to the node for custom styling. If not specified, no CSS class is applied.</param>
        public ANode(string name, string text, string cssClass = "")
        {
            Name = name.Replace(" ", "");
            Text = text;
            CssClass = cssClass;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Name);

            if (!string.IsNullOrEmpty(Text))
            {
                sb.Append(GetSurroundedText());
            }

            return sb.ToString();
        }
        public virtual string ToClassString()
        {
            if (string.IsNullOrEmpty(CssClass))
            {
                return string.Empty;
            }

            return $"class {Name} {CssClass}";
        }
        protected virtual string GetSurroundedText()
        {
            return $"[{Text}]";
        }
    }
}
