using MermaidSharp.Constants;
using System;
using System.Collections.Generic;

namespace MermaidSharp.Diagrams
{
    /// <summary>
    /// Represents the base class for Mermaid diagram generators, providing common properties and methods for creating
    /// and formatting diagram output.
    /// </summary>
    /// <remarks>Derive from this class to implement specific types of Mermaid diagrams. This class defines
    /// the contract for generating diagram content and managing diagram titles.</remarks>
    public abstract class AMermaid
    {
        /// <summary>
        /// Gets the Mermaid name associated with the current instance.
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Gets or sets the title associated with the object.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Initializes a new instance of the AMermaid class with the specified title.
        /// </summary>
        /// <param name="title">The title to assign to the diagram. If not specified, the title is set to an empty string.</param>
        protected AMermaid(string title = "")
        {
            Title = title;
        }

        /// <summary>
        /// Generates a formatted header string for the flowchart title, including separators and the title text, if a
        /// title is specified.
        /// </summary>
        /// <remarks>Override this method to customize the formatting of the flowchart header in derived
        /// classes.</remarks>
        /// <returns>A formatted string containing the flowchart title and separators if the title is not null or empty;
        /// otherwise, an empty string.</returns>
        protected virtual string GetHeaderString()
        {
            if (string.IsNullOrEmpty(Title))
            {
                return string.Empty;
            }
            var lines = new List<string>();
            lines.Add(FormattingConstants.TitleSeparator);
            lines.Add($"title: {Title}");
            lines.Add(FormattingConstants.TitleSeparator);
            return string.Join(Environment.NewLine, lines);
        }

        /// <summary>
        /// Generates the complete Mermaid diagram as a formatted string.
        /// </summary>
        /// <returns>A string containing the full Mermaid diagram.</returns>
        public abstract string CalculateDiagram();
    }
}
