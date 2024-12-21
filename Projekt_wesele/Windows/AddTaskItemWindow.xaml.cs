using System.Windows;
using Projekt_wesele.Models;
using Projekt_wesele.ViewModels;
using Projekt_wesele.ViewModels.AddModels;

namespace Projekt_wesele.Views
{
    public partial class AddTaskItemWindow : Window
    {
        public AddTaskItemWindow(TaskItem item = null)
        {
            InitializeComponent();

            var viewModel = new AddTaskViewModel(item ?? new TaskItem());
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
