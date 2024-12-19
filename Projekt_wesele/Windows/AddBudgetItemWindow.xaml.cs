using System.Windows;
using Projekt_wesele.Models;
using Projekt_wesele.ViewModels;

namespace Projekt_wesele.Views
{
    public partial class AddBudgetItemWindow : Window
    {
        public AddBudgetItemWindow(BudgetItem item = null)
        {
            InitializeComponent();

            var viewModel = new AddBudgetViewModel(item ?? new BudgetItem());
            DataContext = viewModel;

            viewModel.RequestClose += (s, e) =>
            {
                DialogResult = viewModel.DialogResult;
                Close();
            };
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
