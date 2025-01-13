using Projekt_wesele.Data;
using Projekt_wesele.ViewModels;
using System.Windows;

namespace Projekt_wesele
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var context = new WeddingPlannerContext())
            {
                context.EnsureDatabaseCreated();

            }

            DataContext = new MainViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
