using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidDotNet.Attributes
{
    public class MermaidEnumAttribute : Attribute
    {
        public string Start { get; }
        public string End { get; }

        public MermaidEnumAttribute() : this(string.Empty, string.Empty)
        {
        }
        public MermaidEnumAttribute(string start) : this(start, start)
        {
        }
        public MermaidEnumAttribute(string start, string end)
        {
            Start = start;
            End = end;
        }
    }
}
