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

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.Row.Item is Guest editedGuest)
            {
                _viewModel.SaveGuestChanges(editedGuest);
            }
        }

        private void DataGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                if (dataGrid.SelectedItem is Guest modifiedGuest)
                {
                    _viewModel.SaveGuestChanges(modifiedGuest);
                }
            }
        }
    }
}
