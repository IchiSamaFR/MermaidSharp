using MermaidSharp.Configs;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;

namespace MermaidSharp.Samples.Examples
{
    /// <summary>
    /// Provides quadrant chart samples.
    /// </summary>
    public static class QuadrantChartExamples
    {
        public static IEnumerable<DiagramExample> Create()
        {
            yield return Prioritization();
        }

        private static DiagramExample Prioritization()
        {
            var config = new QuadrantChartConfig
            {
                ChartWidth = 500,
                ChartHeight = 500,
                PointRadius = 6,
                XAxisPosition = XAxisPosition.Bottom,
                YAxisPosition = YAxisPosition.Left
            };
            var diagram = new QuadrantChartDiagram("Feature prioritization", config)
            {
                XAxisLeft = "Low effort",
                XAxisRight = "High effort",
                YAxisBottom = "Low impact",
                YAxisTop = "High impact",
                Quadrant1 = "Plan",
                Quadrant2 = "Do first",
                Quadrant3 = "Skip",
                Quadrant4 = "Maybe later"
            };
            diagram.Points.Add(new QuadrantChartPoint { Label = "Dark mode", X = 0.2, Y = 0.8, Color = "#4caf50", Radius = 10 });
            diagram.Points.Add(new QuadrantChartPoint { Label = "SSO", X = 0.8, Y = 0.9, Color = "#2196f3" });
            diagram.Points.Add(new QuadrantChartPoint { Label = "New logo", X = 0.3, Y = 0.2 });
            diagram.Points.Add(new QuadrantChartPoint { Label = "Rewrite", X = 0.9, Y = 0.3, StrokeColor = "#f44336", StrokeWidth = "2px" });

            return new DiagramExample(DiagramKind.QuadrantChart, "quadrant-prioritization", "Feature prioritization",
                "Axis and quadrant labels, styled points and chart configuration.", diagram.CalculateDiagram());
        }
    }
}
