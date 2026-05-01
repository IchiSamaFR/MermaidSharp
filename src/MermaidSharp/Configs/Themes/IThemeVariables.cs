using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Configs.Themes
{
	/// <summary>
	/// Defines theme variable configuration for Mermaid diagrams.
	/// </summary>
	public interface IThemeVariables
	{
		/// <summary>
		/// Returns indented theme variable configuration lines for Mermaid output.
		/// Prepends the <c>themeVariables:</c> section only when theme variable values are present.
		/// </summary>
		/// <returns>
		/// A list of formatted theme variable configuration lines, or an empty list when no theme variables are set.
		/// </returns>
		List<string> GetConfigLines();
	}
}
