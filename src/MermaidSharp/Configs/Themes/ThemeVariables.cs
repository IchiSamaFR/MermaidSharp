using MermaidSharp.Extensions;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MermaidSharp.Configs.Themes
{
    /// <summary>
    /// Represents a collection of Mermaid theme variables used to configure chart styling options.
    /// </summary>
    public abstract class ThemeVariables : AConfigurable
    {
        private readonly string Name = "themeVariables";

        /// <summary>
        /// Returns the theme variable configuration lines for Mermaid output.
        /// Unlike the base implementation, this override indents the parameter lines and prepends the <c>themeVariables:</c> section only when values are present.
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
