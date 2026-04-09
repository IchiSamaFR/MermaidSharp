using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System;
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

		private double? _textPosition;

		/// <summary>
		/// Gets or sets the axial position of the pie slice labels, from 0.0 at the center to 1.0 at the outside edge
		/// of the circle. The default value used by Mermaid is 0.75.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when value is not finite or is not between 0.0 and 1.0 inclusive.</exception>
		public double? TextPosition
		{
			get => _textPosition;
			set
			{
				if (value.HasValue && (double.IsNaN(value.Value) || double.IsInfinity(value.Value) || value.Value < 0.0 || value.Value > 1.0))
					throw new ArgumentOutOfRangeException(nameof(value), "TextPosition must be a finite number between 0.0 and 1.0 inclusive.");
				_textPosition = value;
			}
		}

		public ThemeVariables ThemeVariables { get; set; }

        /// <summary>
        /// Initializes a new instance of the PieChartConfig class with the specified theme and text position.
        /// </summary>
        /// <param name="theme">The visual theme to apply to the diagram. Defaults to <see cref="ConfigTheme.None"/>.</param>
        /// <param name="textPosition">
        /// The axial position of the pie slice labels (0.0 to 1.0 inclusive). If null, Mermaid's default value (0.75) is used.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when textPosition is not finite or is not between 0.0 and 1.0 inclusive.</exception>
        public PieChartConfig(ConfigTheme theme = ConfigTheme.None, double? textPosition = null, ThemeVariables themeVariables = null) : base(theme)
		{
			TextPosition = textPosition;
			ThemeVariables = themeVariables ?? new ThemeVariables();
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
            baseLst.AddRange(ThemeVariables.GetConfigLines().Indent());

            return baseLst;
		}
	}
}