using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;

namespace MermaidSharp.Configs
{
	/// <summary>
	/// Represents the configuration settings for rendering a flow cart diagram.
	/// </summary>
	public class FlowChartConfig : AConfig<FlowChartThemeVariables>
	{
		public FlowChartConfig() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the PieChartConfig class with the specified theme and text position.
		/// </summary>
		/// <param name="theme">The visual theme to apply to the diagram. Defaults to <see cref="ConfigTheme.None"/>.</param>
		/// <param name="themeVariables">
		/// The theme variables to apply to the diagram. If null, a new empty <see cref="PieChartThemeVariables"/> instance is used.
		/// </param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when textPosition is not finite or is not between 0.0 and 1.0 inclusive.</exception>
		public FlowChartConfig(ConfigTheme theme = ConfigTheme.None, FlowChartThemeVariables themeVariables = default) : base(theme, themeVariables)
		{
		}
	}
}
