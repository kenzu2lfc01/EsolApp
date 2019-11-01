using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EsolApp.Services
{
    public interface IJwtService
    {
        IEnumerable<Claim> GetClaim(string token);
    }
}
