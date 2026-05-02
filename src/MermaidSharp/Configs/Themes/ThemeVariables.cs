using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using MermaidSharp.Extensions;

namespace MermaidSharp.Configs.Themes
{
	/// <summary>
	/// Represents a collection of Mermaid theme variables used to configure chart styling options.
	/// </summary>
	public abstract class ThemeVariables : AConfigurable, IThemeVariables
	{
		private readonly string Name = "themeVariables";

		private static readonly System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]> PropertiesCache
			= new System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]>();

		/// <summary>
		/// Gets or sets a value indicating whether dark mode is enabled.
		/// Affects how derived colors are calculated.
		/// </summary>
		[ThemeVariable("darkMode")]
		public bool? DarkMode { get; set; }

		/// <summary>
		/// Gets or sets the background color used to calculate colors for items that should be background colored or contrasting.
		/// </summary>
		[ThemeVariable("background")]
		public string Background { get; set; }

		/// <summary>
		/// Gets or sets the font family used for diagram text. (e.g., "trebuchet ms, verdana, arial")
		/// </summary>
		[ThemeVariable("fontFamily")]
		public string FontFamily { get; set; }

		/// <summary>
		/// Gets or sets the font size used in the diagram. (e.g., "16px")
		/// </summary>
		[ThemeVariable("fontSize")]
		public string FontSize { get; set; }

		/// <summary>
		/// Gets or sets the primary color used as background in nodes.
		/// </summary>
		[ThemeVariable("primaryColor")]
		public string PrimaryColor { get; set; }

		/// <summary>
		/// Gets or sets the text color used in nodes that use the primary color.
		/// </summary>
		[ThemeVariable("primaryTextColor")]
		public string PrimaryTextColor { get; set; }

		/// <summary>
		/// Gets or sets the secondary color, calculated from the primary color.
		/// </summary>
		[ThemeVariable("secondaryColor")]
		public string SecondaryColor { get; set; }

		/// <summary>
		/// Gets or sets the border color used in nodes that use the primary color.
		/// </summary>
		[ThemeVariable("primaryBorderColor")]
		public string PrimaryBorderColor { get; set; }

		/// <summary>
		/// Gets or sets the border color used in nodes that use the secondary color.
		/// </summary>
		[ThemeVariable("secondaryBorderColor")]
		public string SecondaryBorderColor { get; set; }

		/// <summary>
		/// Gets or sets the text color used in nodes that use the secondary color.
		/// </summary>
		[ThemeVariable("secondaryTextColor")]
		public string SecondaryTextColor { get; set; }

		/// <summary>
		/// Gets or sets the tertiary color, calculated from the primary color.
		/// </summary>
		[ThemeVariable("tertiaryColor")]
		public string TertiaryColor { get; set; }

		/// <summary>
		/// Gets or sets the border color used in nodes that use the tertiary color.
		/// </summary>
		[ThemeVariable("tertiaryBorderColor")]
		public string TertiaryBorderColor { get; set; }

		/// <summary>
		/// Gets or sets the text color used in nodes that use the tertiary color.
		/// </summary>
		[ThemeVariable("tertiaryTextColor")]
		public string TertiaryTextColor { get; set; }

		/// <summary>
		/// Gets or sets the background color used in note rectangles.
		/// </summary>
		[ThemeVariable("noteBkgColor")]
		public string NoteBkgColor { get; set; }

		/// <summary>
		/// Gets or sets the text color used in note rectangles.
		/// </summary>
		[ThemeVariable("noteTextColor")]
		public string NoteTextColor { get; set; }

		/// <summary>
		/// Gets or sets the border color used in note rectangles.
		/// </summary>
		[ThemeVariable("noteBorderColor")]
		public string NoteBorderColor { get; set; }

		/// <summary>
		/// Gets or sets the line color, calculated from the background color.
		/// </summary>
		[ThemeVariable("lineColor")]
		public string LineColor { get; set; }

		/// <summary>
		/// Gets or sets the text color used over the background in diagrams, such as labels, signals, and titles.
		/// </summary>
		[ThemeVariable("textColor")]
		public string TextColor { get; set; }

		/// <summary>
		/// Gets or sets the background color for flowchart objects such as rects, circles, and class diagram classes.
		/// </summary>
		[ThemeVariable("mainBkg")]
		public string MainBkg { get; set; }

		/// <summary>
		/// Gets or sets the background color used for syntax error messages.
		/// </summary>
		[ThemeVariable("errorBkgColor")]
		public string ErrorBkgColor { get; set; }

		/// <summary>
		/// Gets or sets the text color used for syntax error messages.
		/// </summary>
		[ThemeVariable("errorTextColor")]
		public string ErrorTextColor { get; set; }

		/// <summary>
		/// Returns indented theme variable configuration lines for Mermaid output.
		/// Prepends the <c>themeVariables:</c> section only when theme variable values are present.
		/// </summary>
		/// <returns>
		/// A list of formatted theme variable configuration lines, or an empty list when no theme variables are set.
		/// </returns>
		public override List<string> GetConfigLines()
		{
			var paramsList = GetParams().Indent();
			if (paramsList.Count == 0)
				return new List<string>();

			paramsList.Insert(0, $"{Name}:");

			return paramsList;
		}

		/// <summary>
		/// Retrieves a list of configuration parameters as formatted strings based on the current settings.
		/// Handles string, double, bool, and List&lt;string&gt; properties decorated with <see cref="ThemeVariableAttribute"/>.
		/// </summary>
		/// <returns>A list of strings representing the configuration parameters.</returns>
		protected sealed override List<string> GetParams()
		{
			var lst = new List<string>();
			var props = PropertiesCache.GetOrAdd(
				GetType(),
				t => t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
					  .Where(p => p.GetCustomAttribute<ThemeVariableAttribute>() != null)
					  .ToArray()
			);

			foreach (var prop in props)
			{
				var attr = prop.GetCustomAttribute<ThemeVariableAttribute>();
				var value = prop.GetValue(this);
				if (value == null)
					continue;

				lst.AddRange(GetProperty(attr, value));
			}

			return lst;
		}

		private IEnumerable<string> GetProperty(ThemeVariableAttribute attr, object value)
		{
			var lst = new List<string>();

			// Handle IEnumerable<string>
			if (value is IEnumerable<string> strList)
			{
				var items = strList.ToList();
				for (int i = 0; i < items.Count; i++)
				{
					var item = items[i];
					if (!string.IsNullOrEmpty(item))
						lst.Add($"{attr.Name.Replace("{index}", (i + 1).ToString())}: \"{item}\"");
				}
			}
			// Handle string
			else if (value is string strVal && !string.IsNullOrEmpty(strVal))
			{
				lst.Add($"{attr.Name}: \"{strVal}\"");
			}
			// Handle double
			else if (value is double dblVal)
			{
				lst.Add($"{attr.Name}: {dblVal.ToString("G", CultureInfo.InvariantCulture)}");
			}
			// Handle bool
			else if (value is bool boolVal)
			{
				lst.Add($"{attr.Name}: {(boolVal ? "true" : "false")}");
			}

			return lst;
		}
	}
}