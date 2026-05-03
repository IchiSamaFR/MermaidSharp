using System;

namespace MermaidSharp.Models
{
    /// <summary>
    /// Represents a data point in a Mermaid quadrant chart.
    /// </summary>
    public class QuadrantChartPoint
    {
        /// <summary>
        /// Gets or sets the label of the point.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate of the point. Value must be between 0 and 1.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the point. Value must be between 0 and 1.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the fill color of the point (e.g. #ff3300).
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the radius of the point.
        /// </summary>
        public int? Radius { get; set; }

        /// <summary>
        /// Gets or sets the border color of the point.
        /// </summary>
        public string StrokeColor { get; set; }

        /// <summary>
        /// Gets or sets the border width of the point (e.g. 5px).
        /// </summary>
        public string StrokeWidth { get; set; }

        /// <summary>
        /// Returns the Mermaid syntax representation of the point.
        /// </summary>
        /// <returns>A string in Mermaid quadrant chart point syntax.</returns>
        public override string ToString()
        {
            var style = BuildStyle();
            string xStr = X.ToString().Replace(',', '.');
            string yStr = Y.ToString().Replace(',', '.');
            return string.IsNullOrEmpty(style)
                ? $"{Label}: [{xStr}, {yStr}]"
                : $"{Label}: [{xStr}, {yStr}] {style}";
        }

        private string BuildStyle()
        {
            var parts = new System.Collections.Generic.List<string>();

            if (!string.IsNullOrEmpty(Color))
                parts.Add($"color: {Color}");
            if (Radius.HasValue)
                parts.Add($"radius: {Radius.Value}");
            if (!string.IsNullOrEmpty(StrokeColor))
                parts.Add($"stroke-color: {StrokeColor}");
            if (!string.IsNullOrEmpty(StrokeWidth))
                parts.Add($"stroke-width: {StrokeWidth}");

            return string.Join(", ", parts);
        }
    }
}