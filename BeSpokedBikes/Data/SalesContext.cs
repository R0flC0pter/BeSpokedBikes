using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Models;

namespace BeSpokedBikes.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext (DbContextOptions<SalesContext> options)
            : base(options)
        {
        }
        public DbSet<Sellers> Sellers{ get; set; }
        public DbSet<Sales> Sales{ get; set; }
        public DbSet<Products> Products{ get; set; }

        public DbSet<Discounts> Discounts { get; set; }

        public DbSet<Customers> Customers{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sellers>().ToTable("Sellers");
            modelBuilder.Entity<Sales>().ToTable("Sales");
            modelBuilder.Entity<Products>().ToTable("Products");
            modelBuilder.Entity<Discounts>().ToTable("Discounts");
            modelBuilder.Entity<Customers>().ToTable("Customers");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=your-database-file.db");
        }
    }
}
