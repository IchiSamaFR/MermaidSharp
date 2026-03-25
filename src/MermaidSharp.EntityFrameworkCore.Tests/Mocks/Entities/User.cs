using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.EntityFrameworkCore.Tests.Mocks.Entities
{
    internal class User : IdentityUser<int>
    {
        public UserAuditInfo<int>? Audit { get; set; }
    }
}
