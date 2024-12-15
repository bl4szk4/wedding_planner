using System;
using System.Windows.Input;
using Projekt_wesele.Helpers;
using Projekt_wesele.Models;

namespace Projekt_wesele.ViewModels
{
    public class AddGuestViewModel : ViewModelBase
    {
        public Guest Guest { get; set; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler RequestClose;
        public bool? DialogResult { get; private set; }

        public AddGuestViewModel(Guest guest = null)
        {
            Guest = guest ?? new Guest();
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Save()
        {
            // Zapisuje zmiany
            DialogResult = true;
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void Cancel()
        {
            // Anuluje zmiany
            DialogResult = false;
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
