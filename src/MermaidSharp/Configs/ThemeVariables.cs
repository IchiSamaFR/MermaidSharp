using MermaidSharp.Extensions;
using System.Collections.Generic;
using System.Globalization;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Represents a collection of Mermaid theme variables used to configure chart styling options.
    /// </summary>
    public class ThemeVariables : AConfigurable
    {
        private readonly string Name = "themeVariables";

        /// <summary>
        /// Gets or sets the font size for the pie chart title text. (e.g., "16px", "1.5em", "large")
        /// </summary>
        public string PieTitleTextSize { get; set; }

        /// <summary>
        /// Gets or sets the color used for the pie chart title text. (e.g., "#RRGGBB", "rgb(255, 0, 0)", "red")
        /// </summary>
        public string PieTitleTextColor { get; set; }

        /// <summary>
        /// Gets or sets the font size for the pie chart section text. (e.g., "16px", "1.5em", "large")
        /// </summary>
        public string PieSectionTextSize { get; set; }

        /// <summary>
        /// Gets or sets the color used for the pie chart section text. (e.g., "#RRGGBB", "rgb(255, 0, 0)", "red")
        /// </summary>
        public string PieSectionTextColor { get; set; }

        /// <summary>
        /// Gets or sets the font size for the pie chart legend text. (e.g., "16px", "1.5em", "large")
        /// </summary>
        public string PieLegendTextSize { get; set; }

        /// <summary>
        /// Gets or sets the color used for the pie chart legend text. (e.g., "#RRGGBB", "rgb(255, 0, 0)", "red")
        /// </summary>
        public string PieLegendTextColor { get; set; }

        /// <summary>
        /// Gets or sets the color used for the pie chart stroke. (e.g., "#RRGGBB", "rgb(255, 0, 0)", "red")
        /// </summary>
        public string PieStrokeColor { get; set; }

        /// <summary>
        /// Gets or sets the width of the pie chart's outer stroke. (e.g., "1px", "0.5em", "thin")
        /// </summary>
        public string PieOuterStrokeWidth { get; set; }

        /// <summary>
        /// Gets or sets the opacity level for the pie chart, where 0.0 is fully transparent and 1.0 is fully opaque.
        /// </summary>
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
