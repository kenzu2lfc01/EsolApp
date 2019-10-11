using EsolApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsolApp.Data
{
    public class EsolAppDbContext : DbContext
    {
        public EsolAppDbContext(DbContextOptions<EsolAppDbContext> options) : base (options)
        {

        }
        public virtual DbSet<Todos> Todos { get; set; }
    }
}
