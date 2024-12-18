using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Projekt_wesele.Models
{
    public class Guest : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private bool _attending;
        private string? _notes;
        private bool _isKid = false;
        private bool _hasPartner = false;
        private GuestSide _side = GuestSide.None;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public bool Attending
        {
            get => _attending;
            set { _attending = value; OnPropertyChanged(); }
        }

        public string? Notes
        {
            get => _notes;
            set { _notes = value; OnPropertyChanged(); }
        }

        public bool IsKid
        {
            get => _isKid;
            set { _isKid = value; OnPropertyChanged(); }
        }

        public bool HasPartner
        {
            get => _hasPartner;
            set { _hasPartner = value; OnPropertyChanged(); }
        }

        public GuestSide Side
        {
            get => _side;
            set { _side = value; OnPropertyChanged(); }
        }

        // Implementacja interfejsu INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum GuestSide
    {
        None = 0,
        Bride = 1,
        Groom = 2
    }
}
