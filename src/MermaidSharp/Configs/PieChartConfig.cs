using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System.Collections.Generic;
using System.Globalization;

namespace MermaidSharp.Configs
{
	/// <summary>
	/// Represents the configuration settings for rendering a Mermaid pie chart diagram.
	/// </summary>
	public class PieChartConfig : AConfig
	{
		private readonly string Name = "pie";

		/// <summary>
		/// Gets or sets the axial position of the pie slice labels, from 0.0 at the center to 1.0 at the outside edge
		/// of the circle. The default value used by Mermaid is 0.75.
		/// </summary>
		public double? TextPosition { get; set; }

		/// <summary>
		/// Initializes a new instance of the PieChartConfig class with the specified theme and text position.
		/// </summary>
		/// <param name="theme">The visual theme to apply to the diagram. Defaults to <see cref="ConfigTheme.None"/>.</param>
		/// <param name="textPosition">
		/// The axial position of the pie slice labels (0.0 to 1.0). If null, Mermaid's default value (0.75) is used.
		/// </param>
		public PieChartConfig(ConfigTheme theme = ConfigTheme.None, double? textPosition = null) : base(theme)
		{
			TextPosition = textPosition;
		}

		/// <summary>
		/// Retrieves a list of configuration parameters as formatted strings based on the current settings.
		/// </summary>
		/// <returns>A list of strings representing the configuration parameters. The list is empty if no parameters are set.</returns>
		protected override List<string> GetParams()
		{
			var baseLst = base.GetParams();
			var lst = new List<string>();

			if (TextPosition != null)
				lst.Add($"textPosition: {TextPosition.Value.ToString("G", CultureInfo.InvariantCulture)}");

			if (lst.Count == 0)
				return baseLst;

			lst = lst.Indent();
			lst.Insert(0, $"{Name}:");
			baseLst.AddRange(lst.Indent());

			return baseLst;
		}
	}
}
