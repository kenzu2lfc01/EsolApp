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
using IdSrv4.Services.ViewRender;
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
        private readonly IViewRenderService _viewRender;
        private readonly AppSettings _appSettings;
        private readonly IHashByMD5 _hashByMD5;
        private readonly IJwtService _jwtService;

        public AccountController(
            AppDbContext appContext,
            IEmailSender emailSender,
            IOptions<AppSettings> appSettings,
            IViewRenderService viewRender,
            IHashByMD5 hashByMD5,
            IJwtService jwtService,
            ILogger<AccountController> logger)
        {
            _appContext = appContext;
            _emailSender = emailSender;
            _hashByMD5 = hashByMD5;
            _jwtService = jwtService;
            _logger = logger;
            _viewRender = viewRender;
            _appSettings = appSettings.Value;
        }
        [HttpGet]
        [Route("GetUsers/{token}")]
        public List<UsersViewModel> GetAllUser(string token)
        {
            IEnumerable<Claim> claims = _jwtService.GetClaim(token);
            Guid UserID = Guid.Parse(claims.FirstOrDefault().Value);
            var temp = _appContext.Users.Where(x => x.Id != UserID).ToList();
            List<UsersViewModel> users = new List<UsersViewModel>();
            foreach(var item in temp)
            {
                UsersViewModel usersViewModel = new UsersViewModel()
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Name = item.Name
                };
                users.Add(usersViewModel);
            }
            return users;
        }
        [HttpPost]
        [Route("Login")]
        //POST : /api/Account/Login
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user =  _appContext.Users.FirstOrDefault(x=> x.UserName == model.UserName);
            if (user != null &&  _hashByMD5.VerifyMd5Hash(model.Password, user.PasswordHash) && user.Status == true)
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
            var user = _appContext.Users.FirstOrDefault(x => x.UserName == model.UserName || x.Email == model.Email);
            if(user != null)
            {
                return BadRequest(new { message = "User name is already" });
            }
            AppUser appUser = new AppUser()
            {
                UserName = model.UserName,
                PasswordHash = _hashByMD5.GetMd5Hash(model.Password),
                Email = model.Email,
                Name = model.Name,
                Status = false
            };
            var result = _appContext.Users.Add(appUser);
            _appContext.SaveChanges();
            //string Mess = await _viewRender.RenderToStringAsync(@"C:\Users\thang.nguyen\Desktop\IonicStudy\EsolApp\IdSrv4\MailBody.cshtml", appUser);
            string Mess = "<a href='https://localhost:44398/api/Account/comfirm/" + appUser.UserName + "'> Comfirm Email Address</a>";
            return Ok(await _emailSender.SendEmailAsync(appUser.Email, "Comfirm your Account", Mess, appUser.Name));
        }
        [HttpGet]
        [Route("comfirm/{username}")]
        public async Task<IActionResult> Comfirm(string username)
        {
            var user = _appContext.Users.FirstOrDefault(x => x.UserName == username);
            user.Status = true;
            _appContext.SaveChanges();
            Response.Redirect("http://localhost:8100/home");
            return Ok();
        }
    }
}