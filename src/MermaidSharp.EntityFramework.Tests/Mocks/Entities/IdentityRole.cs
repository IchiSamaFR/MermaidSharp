using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.EntityFramework.Tests.Mocks.Entities
{
    internal class IdentityRole<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }
}