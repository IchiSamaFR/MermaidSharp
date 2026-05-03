using MermaidSharp.Attributes;
using MermaidSharp.Configs.Enums;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Represents the configuration settings for an XY chart, including theme variables and chart-specific options.
    /// </summary>
    /// <remarks>
    /// Use this class to customize the appearance and behavior of XY charts by specifying theme variables and other configuration parameters.
    /// Inherit from this class to extend or modify chart configuration for specialized scenarios.
    /// </remarks>
    public class XYChartConfig : AConfig<XYChartThemeVariables>
    {
        /// <summary>
        /// Gets the name of the configuration section represented by the derived class.
        /// </summary>
        protected override string SectionName => "xyChart";

        [ConfigVariable("width")]
        /// <summary>
        /// Gets or sets the width of the chart.
        /// </summary>
        public int? Width { get; set; }

        [ConfigVariable("height")]
        /// <summary>
        /// Gets or sets the height of the chart.
        /// </summary>
        public int? Height { get; set; }

        [ConfigVariable("titlePadding")]
        /// <summary>
        /// Gets or sets the top and bottom padding of the title.
        /// </summary>
        public int? TitlePadding { get; set; }

        [ConfigVariable("titleFontSize")]
        /// <summary>
        /// Gets or sets the title font size.
        /// </summary>
        public int? TitleFontSize { get; set; }

        [ConfigVariable("showTitle")]
        /// <summary>
        /// Gets or sets a value indicating whether the title should be shown.
        /// </summary>
        public bool? ShowTitle { get; set; }

        [ConfigVariable("xAxis")]
        /// <summary>
        /// Gets or sets the configuration for the X axis.
        /// </summary>
        public XAxisPosition? XAxis { get; set; }

        [ConfigVariable("yAxis")]
        /// <summary>
        /// Gets or sets the configuration for the Y axis.
        /// </summary>
        public YAxisPosition? YAxis { get; set; }


        [ConfigVariable("chartOrientation")]
        /// <summary>
        /// Gets or sets the chart orientation.
        /// </summary>
        public ChartOrientation ChartOrientationValue { get; set; }

        [ConfigVariable("plotReservedSpacePercent")]
        /// <summary>
        /// Gets or sets the minimum space plots will take inside the chart.
        /// </summary>
        public int? PlotReservedSpacePercent { get; set; }

        [ConfigVariable("showDataLabel")]
        /// <summary>
        /// Gets or sets a value indicating whether to show the value corresponding to the bar within the bar.
        /// </summary>
        public bool? ShowDataLabel { get; set; }

        [ConfigVariable("showDataLabelOutsideBar")]
        /// <summary>
        /// Gets or sets a value indicating whether to show the data label outside the bar.
        /// </summary>
        public bool? ShowDataLabelOutsideBar { get; set; }
    }
}