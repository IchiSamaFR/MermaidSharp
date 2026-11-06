using MermaidSharp.Attributes;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Represents the abstract base type for Mermaid configuration objects in the configuration hierarchy.
    /// </summary>
    public abstract class AConfigurable
    {
        /// <summary>
        /// Returns the mermaid representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            return ConfigSerializer.ToFrontMatter(GetConfigLines());
        }

        /// <summary>
        /// Returns the configuration lines for the current instance as a list of strings.
        /// </summary>
        /// <returns>A list of strings representing the configuration lines. The list is empty if there are no parameters.</returns>
        public abstract List<string> GetConfigLines();

        /// <summary>
        /// Retrieves a list of configuration parameters as formatted strings based on the current settings.
        /// Handles string, double, bool, and List&lt;string&gt; properties decorated with <see cref="ThemeVariableAttribute"/>.
        /// </summary>
        /// <returns>A list of strings representing the configuration parameters.</returns>
        protected List<string> GetThemeVariableParams()
        {
            var lst = new List<string>();
            foreach (var entry in ConfigSerializer.GetDecoratedValues<ThemeVariableAttribute>(this))
            {
                lst.AddRange(GetProperty(entry.Key, entry.Value));
            }

            return lst;
        }

        private static IEnumerable<string> GetProperty(ThemeVariableAttribute attr, object value)
        {
            var lst = new List<string>();

            if (value is IThemeVariables themeVars)
            {
                if (!string.IsNullOrWhiteSpace(attr.Name))
                    throw new InvalidOperationException("Theme variable name must be empty for properties implementing IThemeVariables.");

                lst.AddRange(themeVars.GetConfigLines());
                return lst;
            }

            if (string.IsNullOrWhiteSpace(attr.Name))
                throw new InvalidOperationException("Theme variable name is required for scalar theme variable properties.");

            // Lists are written as numbered variables, e.g. pie1, pie2, ...
            if (value is IEnumerable<string> strList)
            {
                var items = strList.ToList();
                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    if (string.IsNullOrEmpty(item))
                        continue;

                    lst.Add($"{attr.Name.Replace("{index}", (i + 1).ToString(CultureInfo.InvariantCulture))}: {item.ToYamlQuoted()}");
                }

                return lst;
            }

            var formatted = ConfigSerializer.FormatValue(value);
            if (formatted != null)
                lst.Add($"{attr.Name}: {formatted}");

            return lst;
        }
    }
}
