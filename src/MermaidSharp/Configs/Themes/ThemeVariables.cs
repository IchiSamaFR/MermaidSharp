using MermaidSharp.Extensions;
using System.Collections.Generic;

namespace MermaidSharp.Configs.Themes
{
    /// <summary>
    /// Represents a collection of Mermaid theme variables used to configure chart styling options.
    /// </summary>
    public abstract class ThemeVariables : AConfigurable
    {
        private readonly string Name = "themeVariables";

        /// <summary>
        /// Returns indented theme variable configuration lines for Mermaid output.
        /// Prepends the <c>themeVariables:</c> section only when theme variable values are present.
        /// </summary>
        /// <returns>
        /// A list of formatted theme variable configuration lines, or an empty list when no theme variables are set.
        /// </returns>
        public override List<string> GetConfigLines()
        {
            var paramsList = GetParams().Indent();
            if (paramsList.Count == 0)
                return new List<string>();

            paramsList.Insert(0, $"{Name}:");

            return paramsList;
        }
    }
}
