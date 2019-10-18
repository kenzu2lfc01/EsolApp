using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using IdSrv4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Reflection;

namespace IdSrv4.Data
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserClaim<Guid>>().HasKey(x => x.Id);
            builder.Entity<IdentityRoleClaim<Guid>>()
                .HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<Guid>>().HasKey(x => x.UserId);

            builder.Entity<IdentityUserRole<Guid>>()
                .HasKey(x => new { x.RoleId, x.UserId });
            builder.Entity<IdentityUserToken<Guid>>()
               .HasKey(x => new { x.UserId });
            base.OnModelCreating(builder);
        }
    }
}