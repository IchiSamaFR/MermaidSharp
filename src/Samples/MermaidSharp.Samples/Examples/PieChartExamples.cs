using MermaidSharp.Configs;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;

namespace MermaidSharp.Samples.Examples
{
    /// <summary>
    /// Provides pie chart samples.
    /// </summary>
    public static class PieChartExamples
    {
        public static IEnumerable<DiagramExample> Create()
        {
            yield return Basic();
            yield return CustomColors();
        }

        private static DiagramExample Basic()
        {
            var diagram = new PieChartDiagram("Pets adopted by volunteers", showData: true)
                .AddSlice("Dogs", 386)
                .AddSlice("Cats", 85.9)
                .AddSlice("Rats", 15);

            return new DiagramExample(DiagramKind.PieChart, "pie-basic", "Basic pie chart",
                "Slices with decimal values and showData.", diagram.CalculateDiagram());
        }

        private static DiagramExample CustomColors()
        {
            var config = new PieChartConfig(ConfigTheme.Base, textPosition: 0.75, themeVariables: new PieChartThemeVariables
            {
                PieStrokeColor = "#ffffff",
                PieOuterStrokeWidth = "2px",
                PieOpacity = 0.9
            });
            var diagram = new PieChartDiagram("Browser usage", config: config)
                .AddSlice("Chrome", 64.7, "#4285f4")
                .AddSlice("Safari", 18.6, "#34a853")
                .AddSlice("Edge", 5.2, "#0078d7")
                .AddSlice("Firefox", 2.8, "#ff7139")
                .AddSlice("Other", 8.7, "#9e9e9e");

            return new DiagramExample(DiagramKind.PieChart, "pie-colors", "Colors and configuration",
                "Per-slice colors, text position and stroke/opacity theme variables.", diagram.CalculateDiagram());
        }
    }
}
