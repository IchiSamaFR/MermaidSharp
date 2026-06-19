using MermaidSharp.Attributes;
using System;

namespace MermaidSharp.Enums
{
	/// <summary>
	/// Specifies the visibility level of a class property for use in Mermaid diagrams.
	/// </summary>
	/// <remarks>This enumeration is used to indicate the access modifier of a property when generating
	/// Mermaid class diagrams. Each value corresponds to a specific visibility symbol in Mermaid syntax.
	/// Values are powers of two so that the flags can be combined safely with bitwise OR and tested with
	/// <see cref="Enum.HasFlag(Enum)"/> without collisions.</remarks>
	[Flags]
	public enum ClassPropertyVisibility
	{
#pragma warning disable CS1591
		[MermaidEnum("")]
		None = 0,

		[MermaidEnum("+")]
		Public = 1 << 0,

		[MermaidEnum("-")]
		Private = 1 << 1,

		[MermaidEnum("#")]
		Protected = 1 << 2,

		[MermaidEnum("~")]
		Internal = 1 << 3
#pragma warning restore CS1591
	}
}
