using System.Windows.Controls;
using Projekt_wesele.ViewModels;
using Projekt_wesele.Models;

namespace Projekt_wesele.Views
{
    public partial class GuestsListView : UserControl
    {
        private readonly GuestsListViewModel _viewModel;

        public GuestsListView()
        {
            InitializeComponent();
            _viewModel = new GuestsListViewModel();
            DataContext = _viewModel;
        }

    }
}
