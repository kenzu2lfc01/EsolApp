using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdSrv4.Services
{
    public class JwtService : IJwtService
    {
        private string SecrectKey = "MTIzNDU2Nzg5MDEyMzQ1Ng==";
        private SecurityKey GetSecurityKey()
        {
            byte[] sym = Convert.FromBase64String(SecrectKey);
            return new SymmetricSecurityKey(sym);
        }
        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSecurityKey()
            };
        }
        public IEnumerable<Claim> GetClaim(string token)
        {
            TokenValidationParameters tokenValidationParameters = GetValidationParameters();
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validateToken);
                return tokenValid.Claims;
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
