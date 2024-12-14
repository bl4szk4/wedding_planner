using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projekt_wesele.Models;

namespace Projekt_wesele.Data
{
    public class WeddingPlannerContext
    {
        public DbSet<Guest> Gests { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
