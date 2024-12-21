using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Projekt_wesele.ViewModels;

namespace Projekt_wesele.Views
{

    public partial class TasksListView : UserControl

    {
        private readonly TasksListViewModel _viewModel;

        public TasksListView()
        {
            InitializeComponent();
            _viewModel = new TasksListViewModel();
            DataContext = _viewModel;

        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
