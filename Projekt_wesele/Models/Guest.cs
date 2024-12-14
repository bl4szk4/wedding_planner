using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_wesele.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Attending { get; set; }
        public string? Notes { get; set; }
    }
}
