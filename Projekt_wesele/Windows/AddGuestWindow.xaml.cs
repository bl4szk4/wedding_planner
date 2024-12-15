using System.Windows;
using Projekt_wesele.Models;
using Projekt_wesele.ViewModels;

namespace Projekt_wesele.Views
{
    public partial class AddGuestWindow : Window
    {
        public AddGuestWindow(Guest guest = null)
        {
            InitializeComponent();
            var viewModel = new AddGuestViewModel(guest);
            DataContext = viewModel;

            // Obsługa zamknięcia okna z ViewModel
            viewModel.RequestClose += (s, e) =>
            {
                DialogResult = viewModel.DialogResult;
                Close();
            };
        }
    }
}
