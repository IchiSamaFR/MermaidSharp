using MermaidSharp.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Diagrams
{
    public abstract class AGraph : AMermaid
    {
        protected AConfig Config { get; set; }

        public AGraph(string title = "", AConfig config = null) : base(title)
        {
            Config = config;
        }
    }
}
