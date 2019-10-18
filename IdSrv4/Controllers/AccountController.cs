using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IdSrv4.Data;
using IdSrv4.Data.AccountViewModels;
using IdSrv4.Models;
using IdSrv4.Models.AccountViewModels;
using IdSrv4.Services;
using IdSrv4.Services.PasswordHash;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdSrv4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _appContext;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private readonly IHashByMD5 _hashByMD5;

        public AccountController(
            AppDbContext appContext,
            IEmailSender emailSender,
            IOptions<AppSettings> appSettings,
            IHashByMD5 hashByMD5,
            ILogger<AccountController> logger)
        {
            _appContext = appContext;
            _emailSender = emailSender;
            _hashByMD5 = hashByMD5;
            _logger = logger;
            _appSettings = appSettings.Value;
        }
        
        [HttpPost]
        [Route("Login")]
        //POST : /api/Account/Login
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user =  _appContext.Users.FirstOrDefault(x=> x.UserName == model.UserName);
            if (user != null &&  _hashByMD5.VerifyMd5Hash(model.Password, user.PasswordHash))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _appContext.Users.FirstOrDefault(x => x.UserName == model.UserName);
            if(user != null)
            {
                return BadRequest(new { message = "User name is already" });
            }
            AppUser appUser = new AppUser()
            {
                UserName = model.UserName,
                PasswordHash = _hashByMD5.GetMd5Hash(model.Password),
                Email = model.Email
            };
            var result =  _appContext.Users.Add(appUser);
            return Ok(new RegisterResponseViewModel(appUser));
        }
    }
}