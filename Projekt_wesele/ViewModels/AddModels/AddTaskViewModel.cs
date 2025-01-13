using Projekt_wesele.Helpers;
using Projekt_wesele.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Projekt_wesele.ViewModels.AddModels
{
    public class AddTaskViewModel : DialogViewModelBase
    {
        public TaskItem Item { get; set; }

        public AddTaskViewModel(TaskItem item)
        {
            Item = item;
        }

        protected override void Save()
        {
            if (string.IsNullOrWhiteSpace(Item.Name))
            {
                MessageBox.Show("Name is required. Please fill in the Name field.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(Item.Description))
            {
                MessageBox.Show("Description is required. Please fill in the Description field.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            base.Save();
        }
    }
}
