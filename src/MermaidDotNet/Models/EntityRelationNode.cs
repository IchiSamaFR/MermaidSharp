using MermaidDotNet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidDotNet.Models
{
    public class EntityRelationNode : Node
    {
        public List<EntityRelationColumn> Columns { get; set; }
        public EntityRelationNode(string name, List<EntityRelationColumn> columns, string cssClass = "")
            : this(name, "", columns, cssClass)
        {

        }
        public EntityRelationNode(string name, string text, List<EntityRelationColumn> columns, string cssClass = "")
            : base(name, text, cssClass)
        {
            Columns = columns;
        }


        public override string ToString()
        {
            var lines = new List<string>();
            if (Columns.Count == 0)
            {
                return base.ToString();
            }
            lines.Add(string.Join(" ", base.ToString(), "{"));
            lines.AddRange(Columns.Select(c => c.ToString()).Indent());
            lines.Add("}");
            return string.Join(Environment.NewLine, lines);
        }
    }
}
