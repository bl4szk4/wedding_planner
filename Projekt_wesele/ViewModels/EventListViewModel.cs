using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt_wesele.Data;
using Projekt_wesele.Helpers;
using Projekt_wesele.Models;
using Projekt_wesele.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Projekt_wesele.ViewModels;
using Projekt_wesele.ViewModels.AddModels;
using System.Diagnostics;

public class EventListViewModel : ListViewModelBase<Event>
{
    private ObservableCollection<Event> _filteredEvents;

    public EventListViewModel() : base(new WeddingPlannerContext())
    {
    }

    public string? FilterName { get; set; }
    public DateTime? FilterStartTime { get; set; }
    public DateTime? FilterEndTime { get; set; }
    public string? FilterLocation { get; set; }

    public ObservableCollection<Event> FilteredEvents
    {
        get => _filteredEvents;
        set
        {
            _filteredEvents = value;
            OnPropertyChanged(nameof(FilteredEvents));
        }
    }
    protected override void LoadItems()
    {
        var itemsFromDb = _context.Set<Event>()
            .OrderBy(item => item.StartTime)
            .ToList();

        Debug.WriteLine($"Loaded {itemsFromDb.Count} items from database.");

        Items = new ObservableCollection<Event>(itemsFromDb);

        foreach (var item in Items)
        {
            item.PropertyChanged += Item_PropertyChanged;
        }

        Items.CollectionChanged += Items_CollectionChanged;

        OnPropertyChanged(nameof(Items));
    }

    public override void AddItem()
    {
        var addItemWindow = new AddEventWindow();
        if (addItemWindow.ShowDialog() == true)
        {
            var viewModel = addItemWindow.DataContext as AddEventViewModel;
            if (viewModel?.Item != null)
            {
                Items.Add(viewModel.Item);
                _context.Set<Event>().Add(viewModel.Item);
                _context.SaveChanges();
            }
        }
    }

    public override void ApplyFilters()
    {
        var filteredItems = _context.Set<Event>()
            .Where(evt =>
                (string.IsNullOrWhiteSpace(FilterName) || evt.Name.ToLower().Contains(FilterName.ToLower())) &&
                (!FilterStartTime.HasValue || evt.StartTime >= FilterStartTime.Value) &&
                (!FilterEndTime.HasValue || evt.EndTime <= FilterEndTime.Value) &&
                (string.IsNullOrWhiteSpace(FilterLocation) || evt.Location.ToLower().Contains(FilterLocation.ToLower())))
            .OrderBy(evt => evt.StartTime)
            .ToList();

        Items.Clear();
        foreach (var evt in filteredItems)
        {
            Items.Add(evt);
        }
    }

    public override void ClearFilters()
    {
        FilterName = null;
        FilterStartTime = null;
        FilterEndTime = null;
        FilterLocation = null;

        OnPropertyChanged(nameof(FilterName));
        OnPropertyChanged(nameof(FilterStartTime));
        OnPropertyChanged(nameof(FilterEndTime));
        OnPropertyChanged(nameof(FilterLocation));

        base.ClearFilters();
    }
}
