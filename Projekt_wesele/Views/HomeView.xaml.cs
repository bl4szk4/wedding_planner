using System.Windows.Controls;
using Projekt_wesele.ViewModels;

namespace Projekt_wesele.Views
{
    public partial class HomeView : UserControl
    {
        private MainViewModel _viewModel;

        public HomeView(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }
    }
}
