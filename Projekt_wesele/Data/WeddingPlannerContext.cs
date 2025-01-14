using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Projekt_wesele.Models;

namespace Projekt_wesele.Data
{
    public class WeddingPlannerContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var relativePath = @"weddingPlanner.db";
            var absolutePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            optionsBuilder.UseSqlite($"Data Source={absolutePath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Guest>()
                .Property(g => g.Side)
                .HasConversion<int>();

            modelBuilder.Entity<BudgetItem>()
                .Property(b => b.Category)
                .HasConversion<int>();

        }
        public void EnsureDatabaseCreated()
        {
            Database.EnsureCreated();
            try
            {
                Database.Migrate();
            }
            catch (Exception ex)
            {
            }

        }


    }

}
