using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Projekt_wesele.Helpers;
using Projekt_wesele.Models;

namespace Projekt_wesele.ViewModels
{
    public class AddGuestViewModel : INotifyPropertyChanged
    {
        public Guest Guest { get; set; }

        public event EventHandler RequestClose;
        public bool DialogResult { get; private set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SetSideCommand { get; }

        public bool IsBrideSelected
        {
            get => Guest.Side == GuestSide.Bride;
            set
            {
                if (value)
                {
                    Guest.Side = GuestSide.Bride;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsGroomSelected));
                }
            }
        }

        public bool IsGroomSelected
        {
            get => Guest.Side == GuestSide.Groom;
            set
            {
                if (value)
                {
                    Guest.Side = GuestSide.Groom;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsBrideSelected));
                }
            }
        }

        public AddGuestViewModel(Guest guest)
        {
            Guest = guest;
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Save()
        {
            // Logika walidacji i zapisu
            DialogResult = true;
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void Cancel()
        {
            DialogResult = false;
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
