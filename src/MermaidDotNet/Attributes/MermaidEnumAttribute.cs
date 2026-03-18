using System;

namespace MermaidDotNet.Attributes
{
    /// <summary>
    /// Specifies custom start and end delimiters for an enumeration when generating Mermaid diagrams.
    /// </summary>
    /// <remarks>Apply this attribute to enumeration types to control how their values are represented in
    /// Mermaid syntax. The specified start and end strings are used as delimiters around the enumeration values during
    /// diagram generation.</remarks>
    public class MermaidEnumAttribute : Attribute
    {
        public string Start { get; }
        public string End { get; }

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
            Start = start;
            End = string.IsNullOrEmpty(end) ? start : end;
        }
    }
}
