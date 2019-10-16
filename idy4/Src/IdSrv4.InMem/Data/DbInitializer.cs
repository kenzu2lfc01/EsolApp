using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdSrv4.InMem.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        private Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager, Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new IdentityUser()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                }, "123654$");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
