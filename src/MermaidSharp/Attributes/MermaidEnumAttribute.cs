using System;

namespace MermaidSharp.Attributes
{
    /// <summary>
    /// Specifies custom start and end delimiters for an enumeration when generating Mermaid diagrams.
    /// </summary>
    /// <remarks>Apply this attribute to enumeration types to control how their values are represented in
    /// Mermaid syntax. The specified start and end strings are used as delimiters around the enumeration values during
    /// diagram generation.</remarks>
    public class MermaidEnumAttribute : Attribute
    {
        /// <summary>
        /// Gets the primary value associated with this instance.
        /// </summary>
        public string Primary { get; }
        /// <summary>
        /// Gets the secondary value associated with this instance.
        /// </summary>
        public string Secondary { get; }

        /// <summary>
        /// Initializes a new instance of the MermaidEnumAttribute class with optional start and end delimiters for the
        /// enumeration value.
        /// </summary>
        /// <param name="start">The string to use as the starting delimiter for the enumeration value. If not specified, no start delimiter
        /// is applied.</param>
        /// <param name="end">The string to use as the ending delimiter for the enumeration value. If not specified, no end delimiter is
        /// applied.</param>
        public MermaidEnumAttribute(string start = "", string end = "")
        {
            Primary = start;
            Secondary = string.IsNullOrEmpty(end) ? start : end;
        }
    }
}
