using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailStorePrj.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RetailStore>().Property(r => r.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<RetailStore>().HasMany(c=>c.Childs).WithOne(c=>c.CurrentParent);
        }
        public DbSet<RetailStore> RetailStores { set; get; }
        public DbSet<Workstation> Workstations { set; get; }
    }
}
