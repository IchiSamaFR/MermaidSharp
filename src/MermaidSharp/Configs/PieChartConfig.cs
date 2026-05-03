using System;
using System.Collections.Generic;
using System.Globalization;
using MermaidSharp.Attributes;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;
using MermaidSharp.Extensions;

namespace MermaidSharp.Configs
{
	/// <summary>
	/// Represents the configuration settings for rendering a Mermaid pie chart diagram.
	/// </summary>
	public class PieChartConfig : AConfig<PieChartThemeVariables>
    {
        /// <summary>
        /// Gets the name of the configuration section represented by the derived class.
        /// </summary>
		protected override string SectionName => "pie";

		/// <summary>
		/// Gets or sets the axial position of the pie slice labels, from 0.0 at the center to 1.0 at the outside edge
		/// of the circle. The default value used by Mermaid is 0.75.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when value is not finite or is not between 0.0 and 1.0 inclusive.</exception>
		[ConfigVariable("textPosition")]
		public double? TextPosition { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public PieChartConfig() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the PieChartConfig class with the specified theme and text position.
		/// </summary>
		/// <param name="theme">The visual theme to apply to the diagram. Defaults to <see cref="ConfigTheme.None"/>.</param>
		/// <param name="textPosition">
		/// The axial position of the pie slice labels (0.0 to 1.0 inclusive). If null, Mermaid's default value (0.75) is used.
		/// </param>
		/// <param name="themeVariables">
		/// The theme variables to apply to the diagram. If null, a new empty <see cref="PieChartThemeVariables"/> instance is used.
		/// </param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when textPosition is not finite or is not between 0.0 and 1.0 inclusive.</exception>
		public PieChartConfig(ConfigTheme theme = ConfigTheme.None, double? textPosition = null, PieChartThemeVariables themeVariables = default) : base(theme, themeVariables)
		{
			TextPosition = textPosition;
		}
	}
}
