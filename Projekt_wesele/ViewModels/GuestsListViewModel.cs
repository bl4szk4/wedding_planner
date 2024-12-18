using Microsoft.EntityFrameworkCore;
using Projekt_wesele.Data;
using Projekt_wesele.Helpers;
using Projekt_wesele.Models;
using Projekt_wesele.ViewModels;
using Projekt_wesele.Views;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

public class GuestsListViewModel : ViewModelBase
{
    private readonly WeddingPlannerContext _context;

    public ObservableCollection<Guest> Guests { get; set; }
    private List<Guest> _allGuests;

    // Filtry
    public bool? FilterIsKid { get; set; }
    public bool? FilterHasPartner { get; set; }
    public GuestSide? FilterSide { get; set; }
    public ObservableCollection<GuestSide?> AvailableSides { get; set; }

    public ICommand ApplyFiltersCommand { get; }
    public ICommand ClearFiltersCommand { get; }
    public ICommand AddGuestCommand { get; }
    public ICommand DeleteGuestCommand { get; }

    public GuestsListViewModel()
    {
        _context = new WeddingPlannerContext();
        LoadGuests();

        AvailableSides = new ObservableCollection<GuestSide?> { null, GuestSide.Bride, GuestSide.Groom };
        ApplyFiltersCommand = new RelayCommand(ApplyFilters);
        ClearFiltersCommand = new RelayCommand(ClearFilters);
        AddGuestCommand = new RelayCommand(AddGuest);
        DeleteGuestCommand = new RelayCommand<Guest>(DeleteGuest);
    }

    private void LoadGuests()
    {
        var guestsFromDb = _context.Guests.ToList();

        Guests = new ObservableCollection<Guest>(guestsFromDb);

        foreach (var guest in Guests)
        {
            guest.PropertyChanged += Guest_PropertyChanged;
        }

        Guests.CollectionChanged += Guests_CollectionChanged;

        OnPropertyChanged(nameof(Guests));
    }



    private void Guests_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (var newGuest in e.NewItems.Cast<Guest>())
            {
                if (_context.Entry(newGuest).State == EntityState.Detached)
                {
                    _context.Guests.Add(newGuest);
                }
                newGuest.PropertyChanged += Guest_PropertyChanged;
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (var removedGuest in e.OldItems.Cast<Guest>())
            {
                _context.Guests.Remove(removedGuest);
                removedGuest.PropertyChanged -= Guest_PropertyChanged;
            }
        }
    }


    private async void Guest_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (sender is Guest updatedGuest)
        {
            var entry = _context.Entry(updatedGuest);

            if (entry.State == EntityState.Unchanged)
            {
                entry.State = EntityState.Modified;
            }

            await Task.Delay(300);
            _context.SaveChanges();
        }
    }


    private void ApplyFilters()
    {
        var filteredGuests = _context.Guests
            .Where(g =>
                (!FilterIsKid.HasValue || g.IsKid == FilterIsKid.Value) &&
                (!FilterHasPartner.HasValue || g.HasPartner == FilterHasPartner.Value) &&
                (!FilterSide.HasValue || g.Side == FilterSide.Value))
            .ToList();

        Guests.Clear();
        foreach (var guest in filteredGuests)
        {
            Guests.Add(guest);
        }
    }




    private void ClearFilters()
    {
        FilterIsKid = null;
        FilterHasPartner = null;
        FilterSide = null;

        OnPropertyChanged(nameof(FilterIsKid));
        OnPropertyChanged(nameof(FilterHasPartner));
        OnPropertyChanged(nameof(FilterSide));

        ApplyFilters();
    }

    private void AddGuest()
    {
        var addGuestWindow = new AddGuestWindow();
        if (addGuestWindow.ShowDialog() == true)
        {
            var viewModel = addGuestWindow.DataContext as AddGuestViewModel;
            if (viewModel?.Guest != null)
            {
                Guests.Add(viewModel.Guest);
                _context.Guests.Add(viewModel.Guest);
                _context.SaveChanges();
            }
        }
    }

    private void DeleteGuest(Guest guest)
    {
        if (guest != null)
        {
            Guests.Remove(guest);
            _context.Guests.Remove(guest);
            _context.SaveChanges();
        }
    }

}
