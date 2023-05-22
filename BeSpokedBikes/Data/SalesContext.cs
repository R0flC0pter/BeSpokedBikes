using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Models;

namespace BeSpokedBikes.Data
{
    // Represents the database context for the sales data
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {
        }

        // Represents the Sellers table in the database
        public DbSet<Sellers> Sellers { get; set; }

        // Represents the Sales table in the database
        public DbSet<Sales> Sales { get; set; }

        // Represents the Products table in the database
        public DbSet<Products> Products { get; set; }

        // Represents the Discounts table in the database
        public DbSet<Discounts> Discounts { get; set; }

        // Represents the Customers table in the database
        public DbSet<Customers> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure table names for entities
            modelBuilder.Entity<Sellers>().ToTable("Sellers");
            modelBuilder.Entity<Sales>().ToTable("Sales");
            modelBuilder.Entity<Products>().ToTable("Products");
            modelBuilder.Entity<Discounts>().ToTable("Discounts");
            modelBuilder.Entity<Customers>().ToTable("Customers");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database connection
            optionsBuilder.UseSqlite("Data Source=your-database-file.db");
        }
    }
}
