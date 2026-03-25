using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System;
using System.Collections.Generic;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Provides a base class for configuration objects that generate Mermaid configuration blocks with optional theming
    /// support.
    /// </summary>
    /// <remarks>This abstract class is intended to be inherited by specific configuration types that require
    /// Mermaid-compatible configuration output. It manages the formatting of configuration parameters and supports
    /// theming through the Theme property. Derived classes should override the GetParams method to supply additional
    /// configuration parameters as needed.</remarks>
    public abstract class AConfig
    {
        private readonly string Name = "config";
        /// <summary>
        /// Gets or sets the theme configuration for the Mermaid diagram rendering.
        /// </summary>
        public ConfigTheme Theme { get; set; }

        /// <summary>
        /// Initializes a new instance of the AConfig class with the specified theme.
        /// </summary>
        /// <param name="theme">The theme to apply to the configuration.</param>
        public AConfig(ConfigTheme theme = ConfigTheme.None)
        {
            Theme = theme;
        }

        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            return GetConfig();
        }

        private string GetConfig()
        {
            var paramsList = GetParams();
            if (paramsList.Count == 0)
                return string.Empty;

            paramsList.Insert(0, $"{Name}:");

            // Surround parameters with "---" to create a block in Mermaid syntax
            paramsList.Insert(0, "---");
            paramsList.Add("---");

            return string.Join(Environment.NewLine, paramsList);
        }

        /// <summary>
        /// Retrieves a list of configuration parameters as formatted strings based on the current theme settings.
        /// </summary>
        /// <remarks>Override this method in a derived class to include additional configuration
        /// parameters as needed.</remarks>
        /// <returns>A list of strings representing the configuration parameters. The list is empty if no parameters are set.</returns>
        protected virtual List<string> GetParams()
        {
            var lst = new List<string>();
            if (Theme != ConfigTheme.None)
                lst.Add($"theme: {Theme.ToString().ToLower()}");
            return lst.Indent();
        }
    }
}
