using System.Collections.Generic;
using MermaidSharp.EntityFrameworkCore.Contexts;

namespace MermaidSharp.EntityFrameworkCore.Models
{
	internal class DiagramTable
	{
		public EntityTypeContext EntityType { get; set; }
		public string Name { get; set; }
		public List<DiagramColumn> Columns { get; set; } = new List<DiagramColumn>();
	}
}
