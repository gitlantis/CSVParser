using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVParser.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Employee table
            modelBuilder.Entity<Employee>().ToTable("Employee");

            // Employee primary key
            modelBuilder.Entity<Employee>().HasKey(u => u.Id).HasName("PK_Employees");
        }
    }
}
