using MermaidSharp.Configs;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;

namespace MermaidSharp.Samples.Examples
{
    /// <summary>
    /// Provides XY chart samples.
    /// </summary>
    public static class XYChartExamples
    {
        public static IEnumerable<DiagramExample> Create()
        {
            yield return BarAndLine();
            yield return Horizontal();
        }

        private static DiagramExample BarAndLine()
        {
            var diagram = new XYChartDiagram("Monthly revenue", "Month", "Revenue (k$)");
            diagram.XAxis.Labels.AddRange(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun" });
            diagram.AddSeries(XYSeriesType.Bar).AddPoints(42, 51, 48, 60, 72, 69);
            diagram.AddSeries(XYSeriesType.Line).AddPoints(40, 45, 50, 55, 60, 65);

            return new DiagramExample(DiagramKind.XYChart, "xy-bar-line", "Bars and line",
                "Categorical x-axis with a bar series and a trend line.", diagram.CalculateDiagram());
        }

        private static DiagramExample Horizontal()
        {
            var config = new XYChartConfig
            {
                ChartOrientation = ChartOrientation.Horizontal,
                ShowDataLabel = true,
                Width = 700,
                Height = 400
            };
            var diagram = new XYChartDiagram("Survey score", "Team", "Score", config);
            diagram.XAxis.Labels.AddRange(new[] { "Alpha", "Beta", "Gamma" });
            diagram.YAxis.Min = 0;
            diagram.YAxis.Max = 10;
            var series = diagram.AddSeries(XYSeriesType.Bar);
            series.SetPoint("Alpha", 7.5);
            series.SetPoint("Beta", 8.2);
            series.SetPoint("Gamma", 6.1);

            return new DiagramExample(DiagramKind.XYChart, "xy-horizontal", "Horizontal bars",
                "Horizontal orientation, data labels, y-axis range and points set by label.", diagram.CalculateDiagram());
        }
    }
}
