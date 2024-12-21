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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using Projekt_wesele.ViewModels;
using Microsoft.EntityFrameworkCore;
using Projekt_wesele.ViewModels.AddModels;
using System.Windows;

public class BudgetListViewModel : ListViewModelBase<BudgetItem>
{
    public bool? FilterIsPayed { get; set; }
    public bool? FilterIsEssential { get; set; }
    public BudgetItemCategory? FilterCategory { get; set; }
    public decimal? MinCost {  get; set; }
    public decimal? MaxCost { get; set; }
    public ObservableCollection<BudgetItemCategory?> AvailableCategories { get; set; }
    public ICommand GenerateBudgetRaportCommand { get; }


    public BudgetListViewModel() : base(new WeddingPlannerContext())
    {
        GenerateBudgetRaportCommand = new RelayCommand(GenerateBudgetRaport);

        AvailableCategories = new ObservableCollection<BudgetItemCategory?> { 
            null,
            BudgetItemCategory.Services,
            BudgetItemCategory.Drinks,
            BudgetItemCategory.Food,
            BudgetItemCategory.Other,
            BudgetItemCategory.Clothes,
            BudgetItemCategory.Decorations,
            BudgetItemCategory.Sweets,
            BudgetItemCategory.Hall,
        };
    }

    public override void ApplyFilters()
    {
        var filteredItems = _context.Set<BudgetItem>()
            .Where(item =>
                (!FilterIsPayed.HasValue || item.IsPayed == FilterIsPayed.Value) &&
                (!FilterIsEssential.HasValue || item.IsEssential == FilterIsEssential.Value) &&
                (!FilterCategory.HasValue || item.Category == FilterCategory.Value) &&
                (!MinCost.HasValue || item.Cost >= MinCost.Value) &&
                (!MaxCost.HasValue || item.Cost <= MaxCost.Value))
            .ToList();

        Items.Clear();
        foreach (var item in filteredItems)
        {
            Items.Add(item);
        }
    }

    public void GenerateBudgetRaport()
    {
        var totalCost = Items.Sum(item => item.Cost);

        var customMessageBox = new BudgetMessageBox($"Total cost of filtered items: {totalCost:C}", Items);
        customMessageBox.ShowDialog();
    }

    public override void AddItem()
    {
        var addItemWindow = new AddBudgetItemWindow();
        if (addItemWindow.ShowDialog() == true)
        {
            var viewModel = addItemWindow.DataContext as AddBudgetViewModel;
            if (viewModel?.Item != null)
            {
                Items.Add(viewModel.Item);
                _context.Set<BudgetItem>().Add(viewModel.Item);
                _context.SaveChanges();
            }
        }
    }

    public override void ClearFilters()
    {
        FilterIsPayed = null;
        FilterIsEssential = null;
        FilterCategory = null;
        MinCost = null;
        MaxCost = null;

        OnPropertyChanged(nameof(FilterIsPayed));
        OnPropertyChanged(nameof(FilterIsEssential));
        OnPropertyChanged(nameof(FilterCategory));
        OnPropertyChanged(nameof(MinCost));
        OnPropertyChanged(nameof(MaxCost));
        base.ClearFilters();

    }
}
