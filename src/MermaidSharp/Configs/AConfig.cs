using MermaidSharp.Enums;
using MermaidSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Configs
{
    public abstract class AConfig
    {
        private readonly string Name = "config";
        public ConfigTheme Theme { get; set; }

        public AConfig(ConfigTheme theme = ConfigTheme.None)
        {
            Theme = theme;
        }

        public override string ToString()
        {
            return GetConfig();
        }

        protected string GetConfig()
        {
            var paramsList = GetParams();
            if (paramsList.Count == 0)
                return string.Empty;
            
            paramsList.Insert(0, $"{Name}:");

            // Surround parameters with "---" to create a block in Mermaid syntax
            paramsList.Insert(0, "---");
            paramsList.Add("---");

            return string.Join(Environment.NewLine, paramsList);
        }

        protected virtual List<string> GetParams()
        {
            var lst = new List<string>();
            if (Theme != ConfigTheme.None)
                lst.Add($"theme: {Theme.ToString().ToLower()}");
            return lst.Indent();
        }
    }
}
