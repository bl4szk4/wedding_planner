using Projekt_wesele.ViewModels;
using System.Windows;

namespace Projekt_wesele
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(); // Powiązanie z MainViewModel
        }
    }
}
