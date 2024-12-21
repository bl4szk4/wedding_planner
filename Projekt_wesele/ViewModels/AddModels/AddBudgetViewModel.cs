using Projekt_wesele.Helpers;
using Projekt_wesele.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Projekt_wesele.ViewModels.AddModels
{
    public class AddBudgetViewModel : DialogViewModelBase
    {
        public BudgetItem Item { get; set; }

        public ObservableCollection<BudgetItemCategory> AvailableCategories { get; }

        public ICommand SetCategoryCommand { get; }

        public AddBudgetViewModel(BudgetItem item)
        {
            Item = item;

            AvailableCategories = new ObservableCollection<BudgetItemCategory>
            {
                BudgetItemCategory.Decorations,
                BudgetItemCategory.Hall,
                BudgetItemCategory.Clothes,
                BudgetItemCategory.Food,
                BudgetItemCategory.Sweets,
                BudgetItemCategory.Drinks,
                BudgetItemCategory.Services,
                BudgetItemCategory.Other
            };

            SetCategoryCommand = new RelayCommand<BudgetItemCategory>(SetCategory);
        }

        private void SetCategory(BudgetItemCategory category)
        {
            Item.Category = category;

            OnPropertyChanged(nameof(Item.Category));
        }

        protected override void Save()
        {
            if (string.IsNullOrWhiteSpace(Item.Name))
            {
                MessageBox.Show("Name is required. Please fill in the Name field.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            base.Save();
        }
    }
}
