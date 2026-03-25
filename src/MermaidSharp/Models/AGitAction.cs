using MermaidSharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Models
{
    public abstract class AGitAction
    {
        public abstract string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
