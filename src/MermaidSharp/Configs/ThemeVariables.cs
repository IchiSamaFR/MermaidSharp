using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Represents a collection of Mermaid theme variables used to configure chart styling options.
    /// </summary>
    public class ThemeVariables : AConfigurable
    {
        private readonly string Name = "themeVariables";

        public string PieTitleTextSize { get; set; }
        public string PieTitleTextColor { get; set; }
        public string PieSectionTextSize { get; set; }
        public string PieSectionTextColor { get; set; }
        public string PieLegendTextSize { get; set; }
        public string PieLegendTextColor { get; set; }
        public string PieStrokeColor { get; set; }
        public string PieOuterStrokeWidth { get; set; }
        public double? PieOpacity { get; set; }

        /// <summary>
        /// Retrieves a list of configuration parameters as formatted strings based on the current settings.
        /// </summary>
        /// <returns>A list of strings representing the configuration parameters. The list is empty if no parameters are set.</returns>
        protected override List<string> GetParams()
        {
            var lst = new List<string>();

            if (!string.IsNullOrEmpty(PieTitleTextSize))
                lst.Add($"pieTitleTextSize: \"{PieTitleTextSize}\"");

            if (!string.IsNullOrEmpty(PieTitleTextColor))
                lst.Add($"pieTitleTextColor: \"{PieTitleTextColor}\"");

            if (!string.IsNullOrEmpty(PieSectionTextSize))
                lst.Add($"pieSectionTextSize: \"{PieSectionTextSize}\"");

            if (!string.IsNullOrEmpty(PieSectionTextColor))
                lst.Add($"pieSectionTextColor: \"{PieSectionTextColor}\"");

            if (!string.IsNullOrEmpty(PieLegendTextSize))
                lst.Add($"pieLegendTextSize: \"{PieLegendTextSize}\"");

            if (!string.IsNullOrEmpty(PieLegendTextColor))
                lst.Add($"pieLegendTextColor: \"{PieLegendTextColor}\"");

            if (!string.IsNullOrEmpty(PieStrokeColor))
                lst.Add($"pieStrokeColor: \"{PieStrokeColor}\"");

            if (!string.IsNullOrEmpty(PieOuterStrokeWidth))
                lst.Add($"pieOuterStrokeWidth: \"{PieOuterStrokeWidth}\"");

            if (PieOpacity.HasValue)
                lst.Add($"pieOpacity: {PieOpacity.Value.ToString("G", CultureInfo.InvariantCulture)}");

            return lst;
        }

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
