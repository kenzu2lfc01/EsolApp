using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdSrv4.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
    }
}
