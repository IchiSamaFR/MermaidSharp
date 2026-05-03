using System;
using System.Collections.Generic;


namespace MermaidSharp.Configs
{
	/// <summary>
	/// Represents the abstract base type for Mermaid configuration objects in the configuration hierarchy.
	/// </summary>
	public abstract class AConfigurable
	{
		/// <summary>
		/// Returns the mermaid representation of the current instance.
		/// </summary>
		public override string ToString()
		{
			var lines = GetConfigLines();
			if (lines.Count == 0)
				return string.Empty;

			lines.Insert(0, $"---");
			lines.Add("---");

			return string.Join(Environment.NewLine, lines);
		}

		/// <summary>
		/// Returns the configuration lines for the current instance as a list of strings.
		/// </summary>
		/// <returns>A list of strings representing the configuration lines. The list is empty if there are no parameters.</returns>
		public abstract List<string> GetConfigLines();

		/// <summary>
		/// Retrieves a list of configuration parameters as formatted strings based on the current theme settings.
		/// </summary>
		/// <remarks>Override this method in a derived class to include additional configuration
		/// parameters as needed.</remarks>
		/// <returns>A list of strings representing the configuration parameters. The list is empty if no parameters are set.</returns>
		protected abstract List<string> GetThemeVariableParams();
	}
}
