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

public class TasksListViewModel : ListViewModelBase<TaskItem>
{
    private ObservableCollection<TaskItem> _filteredTaskItems;

    public TasksListViewModel() : base(new WeddingPlannerContext())
    {
    }

    public string? FilterName { get; set; }
    public bool? FilterIsCompleted { get; set; }

    public override void AddItem()
    {
        var addItemWindow = new AddTaskItemWindow();
        if (addItemWindow.ShowDialog() == true)
        {
            var viewModel = addItemWindow.DataContext as AddTaskViewModel;
            if (viewModel?.Item != null)
            {
                Items.Add(viewModel.Item);
                _context.Set<TaskItem>().Add(viewModel.Item);
                _context.SaveChanges();
            }
        }
    }

    protected override void LoadItems()
    {
        var itemsFromDb = _context.Set<TaskItem>()
            .OrderBy(task => task.IsCompleted)
            .ToList();


        Items = new ObservableCollection<TaskItem>(itemsFromDb);

        foreach (var item in Items)
        {
            item.PropertyChanged += Item_PropertyChanged;
        }

        Items.CollectionChanged += Items_CollectionChanged;

        OnPropertyChanged(nameof(Items));
    }

    public override void ApplyFilters()
    {
        var filteredItems = _context.Set<TaskItem>()
            .Where(task =>
                (string.IsNullOrWhiteSpace(FilterName) || task.Name.ToLower().Contains(FilterName.ToLower())) &&
                (!FilterIsCompleted.HasValue || task.IsCompleted == FilterIsCompleted.Value))
            .ToList();

        Items.Clear();
        foreach (var task in filteredItems)
        {
            Items.Add(task);
        }
    }


    public override void ClearFilters()
    {
        FilterIsCompleted = null;
        FilterName = null;

        OnPropertyChanged(nameof(FilterName));
        OnPropertyChanged(nameof(FilterIsCompleted));
        base.ClearFilters();
    }
}
