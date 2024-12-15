using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Projekt_wesele.Data;
using Projekt_wesele.Helpers;
using Projekt_wesele.Models;
using Projekt_wesele.Views;

namespace Projekt_wesele.ViewModels
{
    public class GuestsListViewModel : ViewModelBase
    {
        private readonly WeddingPlannerContext _context;

        public ObservableCollection<Guest> Guests { get; set; }

        public ICommand AddGuestCommand { get; }
        public ICommand DeleteGuestCommand { get; }
        public ICommand EditGuestCommand { get; }

        public GuestsListViewModel()
        {
            _context = new WeddingPlannerContext();

            Guests = new ObservableCollection<Guest>(_context.Guests.ToList());

            AddGuestCommand = new RelayCommand(AddGuest);
            DeleteGuestCommand = new RelayCommand<Guest>(DeleteGuest);
            EditGuestCommand = new RelayCommand<Guest>(EditGuest);
        }

        private void AddGuest()
        {
            var addGuestWindow = new AddGuestWindow();
            if (addGuestWindow.ShowDialog() == true)
            {
                var viewModel = addGuestWindow.DataContext as AddGuestViewModel;
                if (viewModel != null)
                {
                    var newGuest = viewModel.Guest;
                    _context.Guests.Add(newGuest);
                    _context.SaveChanges();
                    Guests.Add(newGuest);
                }
            }
        }

        private void DeleteGuest(Guest guest)
        {
            if (guest != null)
            {
                MessageBoxResult result = MessageBox.Show($"Czy na pewno chcesz usunąć gościa {guest.Name}?",
                    "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _context.Guests.Remove(guest);
                    _context.SaveChanges();
                    Guests.Remove(guest);
                }
            }
        }

        private void EditGuest(Guest guest)
        {
            if (guest != null)
            {
                var editGuestWindow = new AddGuestWindow(guest);
                if (editGuestWindow.ShowDialog() == true)
                {
                    var viewModel = editGuestWindow.DataContext as AddGuestViewModel;
                    if (viewModel != null)
                    {
                        guest.Name = viewModel.Guest.Name;
                        guest.Attending = viewModel.Guest.Attending;
                        guest.Notes = viewModel.Guest.Notes;
                        guest.IsKid = viewModel.Guest.IsKid;
                        guest.HasPartner = viewModel.Guest.HasPartner;

                        _context.Guests.Update(guest);
                        _context.SaveChanges();
                    }
                }
            }
        }

        public void SaveGuestChanges(Guest guest)
        {
            if (guest != null)
            {
                _context.Guests.Update(guest);
                _context.SaveChanges();
            }
        }
    }
}
