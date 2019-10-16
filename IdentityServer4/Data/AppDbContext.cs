using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);

        //    builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
        //        .HasKey(x => x.Id);

        //    builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

        //    builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
        //        .HasKey(x => new { x.RoleId, x.UserId });

        //    builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
        //       .HasKey(x => new { x.UserId });
        //    base.OnModelCreating(builder);
        //}
    }
}
