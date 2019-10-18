using IdSrv4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace IdSrv4.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //var context = serviceProvider.GetRequiredService<AppDbContext>();
            //context.Database.EnsureCreated();
            //context.Users.Add(new AppUser()
            //{
            //    Id = Guid.Parse("561954bf-ca55-4acb-a50c-3c7a180115dc"),
            //    UserName = "admin",
            //    Email = "nkoxken65@gmail.com",
            //    PasswordHash = "62c8ad0a15d9d1ca38d5dee762a16e01",
            //    PhoneNumber = "0339445627"
            //});
            //context.Roles.Add(new AppRole() 
            //{
            //    Id = Guid.Parse("a142092d-0d75-4636-bac8-57897db3c6e1"),
            //    Name = "administrator",
            //    NormalizedName = "ADMIN"
                
            //});
            //context.UserRoles.Add(new AppUserRole()
            //{
            //    RoleId = Guid.Parse("a142092d-0d75-4636-bac8-57897db3c6e1"),
            //    UserId = Guid.Parse("561954bf-ca55-4acb-a50c-3c7a180115dc")
            //});
            //context.SaveChanges();
        }
    }
}
