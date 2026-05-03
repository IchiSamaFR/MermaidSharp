using MermaidSharp.Configs;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using System;
using System.Collections.Generic;

namespace MermaidSharp.Diagrams
{
    /// <summary>
    /// Represents a Mermaid XY chart diagram, composed of axes, series, and data points.
    /// </summary>
    /// <remarks>
    /// Use this class to build and render XY chart diagrams by programmatically adding series and configuring axes.
    /// </remarks>
    public class XYChartDiagram : AMermaid<XYChartConfig>
    {
        /// <summary>
        /// Gets the Mermaid keyword for the XY chart diagram type.
        /// </summary>
        protected override string Name => "xychart";

        /// <summary>
        /// Gets the collection of labels for the X-axis.
        /// </summary>
        public ChartXAxis XAxis { get; }

        /// <summary>
        /// Gets the Y-axis configuration for the chart.
        /// </summary>
        public ChartYAxis YAxis { get; }

        /// <summary>
        /// Gets the collection of series contained in this XY chart.
        /// </summary>
        public List<XYSeries> Series { get; } = new List<XYSeries>();

        /// <summary>
        /// Initializes a new instance of the XYChartDiagram class with the specified title and configuration.
        /// </summary>
        /// <param name="title">The title of the XY chart. If not specified, no title is rendered.</param>
        /// <param name="titleXAxis">The title of the X-axis. If not specified, no title is rendered.</param>
        /// <param name="titleYAxis">The title of the Y-axis. If not specified, no title is rendered.</param>
        /// <param name="config">The configuration settings to apply to the XY chart. If null, default settings are used.</param>
        public XYChartDiagram(string title = "", string titleXAxis = null, string titleYAxis = null, XYChartConfig config = null) : base(title, config)
        {
            if (string.IsNullOrWhiteSpace(titleXAxis))
				titleXAxis = "xAxis";

            if (string.IsNullOrWhiteSpace(titleYAxis))
				titleYAxis = "yAxis";

            XAxis = new ChartXAxis(titleXAxis);
            YAxis = new ChartYAxis(titleYAxis);
        }

		/// <summary>
		/// Adds a series to the XY chart.
		/// </summary>
		/// <param name="seriesType">The type of the series to add.</param>
		/// <returns>The newly added XYSeries instance.</returns>
		public XYSeries AddSeries(XYSeriesType seriesType)
        {
            var series = new XYSeries(seriesType);
			series.SetXAxis(XAxis);
            Series.Add(series);
            return series;
        }

        /// <summary>
        /// Generates the Mermaid diagram as a formatted string.
        /// </summary>
        /// <returns>A string containing the full Mermaid XY chart diagram.</returns>
        public override string CalculateDiagram()
        {
            var lines = new List<string>
            {
                GetHeaderString(),
                Name
            };

            lines.Add(XAxis.ToString());
            lines.Add(YAxis.ToString());

            // Ajoute les séries
            foreach (var series in Series)
            {
                lines.Add(series.ToString());
            }

            return string.Join(Environment.NewLine, lines);
        }
    }
}