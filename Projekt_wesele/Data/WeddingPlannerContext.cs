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
            //var logFilePath = @"D:\Studia\Informatyka\C#\Projekt_wesele\Projekt_wesele\Projekt_wesele\weddingPlanner.log";

            //var fileLogger = new StreamWriter(logFilePath, append: true)
            //{
            //    AutoFlush = true
            //};
            optionsBuilder.UseSqlite(@"Data Source=D:\Studia\Informatyka\C#\Projekt_wesele\Projekt_wesele\Projekt_wesele\weddingPlanner.db");
                    //.LogTo(fileLogger.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
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

    }

}
