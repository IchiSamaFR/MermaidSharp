using MermaidSharp.Configs;
using System.Collections.Generic;

namespace MermaidSharp.Diagrams
{
    /// <summary>
    /// Represents the base class for all Mermaid graph types, providing common configuration and title support.
    /// </summary>
    /// <remarks>This abstract class serves as the foundation for specific graph implementations within the
    /// MermaidSharp library. It encapsulates shared configuration logic and ensures consistent handling of graph titles
    /// and settings across derived types.</remarks>
    public abstract class AGraph : AMermaid
    {
        /// <summary>
        /// Gets or sets the configuration settings for the current instance.
        /// </summary>
        protected AConfig Config { get; set; }

        /// <summary>
        /// Initializes a new instance of the AGraph class with the specified title and configuration.
        /// </summary>
        /// <param name="title">The title to assign to the graph. If not specified, an empty string is used.</param>
        /// <param name="config">The configuration settings to apply to the graph. If null, default configuration is used.</param>
        public AGraph(string title = "", AConfig config = null) : base(title)
        {
            Config = config;
        }

        /// <summary>
        /// Creates a list of configuration lines representing the current object's settings.
        /// </summary>
        /// <returns>A list of strings containing configuration lines.</returns>
        protected override List<string> GetConfigLines()
        {
            var lines = base.GetConfigLines();
            lines.AddRange(Config?.GetConfigLines() ?? new List<string>());
            return lines;
        }
    }
}
