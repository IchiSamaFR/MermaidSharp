using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.EntityFrameworkCore.Tests.Mocks.Entities
{
    internal class UserAuditInfo<T>
    {
        public int Id { get; set; }
        public T CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public T? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
