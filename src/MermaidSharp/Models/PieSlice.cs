using System;
using System.Globalization;

namespace MermaidSharp.Models
{
	/// <summary>
	/// Represents a single slice in a Mermaid pie chart diagram, defined by a label and a positive numeric value.
	/// </summary>
	public class PieSlice
	{
		/// <summary>
		/// Gets the label for this pie slice.
		/// </summary>
		public string Label { get; }

		/// <summary>
		/// Gets the numeric value of this pie slice. Supported up to two decimal places.
		/// </summary>
		public double Value { get; }

		/// <summary>
		/// Initializes a new instance of the PieSlice class with the specified label and value.
		/// </summary>
		/// <param name="label">The display label for the pie slice.</param>
		/// <param name="value">The positive numeric value for the pie slice. Supported up to two decimal places.</param>
		/// <exception cref="ArgumentNullException">Thrown when label is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when value is not greater than zero.</exception>
		public PieSlice(string label, double value)
		{
			if (label == null)
				throw new ArgumentNullException(nameof(label));
			if (value <= 0)
				throw new ArgumentOutOfRangeException(nameof(value), "Pie slice value must be greater than zero.");

			Label = label;
			Value = value;
		}

		/// <summary>
		/// Returns the Mermaid representation of this pie slice.
		/// </summary>
		/// <returns>A string in the format <c>"Label" : Value</c>.</returns>
		public override string ToString()
		{
			return $"\"{Label}\" : {Value.ToString("0.##", CultureInfo.InvariantCulture)}";
		}
	}
}
