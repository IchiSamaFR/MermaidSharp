using MermaidDotNet.Enums;
using MermaidDotNet.Extensions;
using System.Collections.Generic;

namespace MermaidDotNet.Models
{
    public class EntityRelationColumn
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ColumnKeyType ColumnKeyType { get; set; }
        public string Comment { get; set; }
        public EntityRelationColumn(string name, string type = "", ColumnKeyType columnKeyType = ColumnKeyType.None, string comment = "")
        {
            Name = name;
            Type = type;
            ColumnKeyType = columnKeyType;
            Comment = comment;
        }

        public string ToString()
        {
            var comment = !string.IsNullOrEmpty(Comment) ? $"\"{Comment}\"" : string.Empty;

            var returnedParts = new string[]
            {
                Type,
                Name,
                ColumnKeyType.StartString(),
                comment
            };
            return returnedParts.JoinNonEmpty(" ");
        }
    }
}
