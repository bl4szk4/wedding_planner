using System;

namespace Projekt_wesele.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Attending { get; set; }
        public string? Notes { get; set; }
        public bool IsKid { get; set; } = false;
        public bool HasPartner { get; set; } = false;

        public GuestSide Side { get; set; } = GuestSide.None;
    }

    public enum GuestSide
    {
        None = 0,
        Bride = 1, 
        Groom = 2
    }
}
