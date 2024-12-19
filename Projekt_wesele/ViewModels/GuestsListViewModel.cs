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

public class GuestsListViewModel : ListViewModelBase<Guest>
{
    public bool? FilterIsKid { get; set; }
    public bool? FilterHasPartner { get; set; }
    public bool? FilterIsAttending { get; set; }
    public GuestSide? FilterSide { get; set; }
    public ObservableCollection<GuestSide?> AvailableSides { get; set; }

    public GuestsListViewModel() : base(new WeddingPlannerContext())
    {
        AvailableSides = new ObservableCollection<GuestSide?> { null, GuestSide.Bride, GuestSide.Groom };
    }

    public override void ApplyFilters()
    {
        var filteredGuests = _context.Set<Guest>()
            .Where(g =>
                (!FilterIsKid.HasValue || g.IsKid == FilterIsKid.Value) &&
                (!FilterHasPartner.HasValue || g.HasPartner == FilterHasPartner.Value) &&
                (!FilterSide.HasValue || g.Side == FilterSide.Value) &&
                (!FilterIsAttending.HasValue || g.Attending == FilterIsAttending.Value))
            .ToList();

        Items.Clear();
        foreach (var guest in filteredGuests)
        {
            Items.Add(guest);
        }
    }

    public override void AddItem()
    {
        var addGuestWindow = new AddGuestWindow();
        if (addGuestWindow.ShowDialog() == true)
        {
            var viewModel = addGuestWindow.DataContext as AddGuestViewModel;
            if (viewModel?.Guest != null)
            {
                Items.Add(viewModel.Guest);
                _context.Set<Guest>().Add(viewModel.Guest);
                _context.SaveChanges();
            }
        }
    }

    public override void ClearFilters()
    {
        FilterIsKid = null;
        FilterHasPartner = null;
        FilterSide = null;

        OnPropertyChanged(nameof(FilterIsKid));
        OnPropertyChanged(nameof(FilterHasPartner));
        OnPropertyChanged(nameof(FilterSide));
        base.ClearFilters();

    }
}
