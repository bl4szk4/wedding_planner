using Projekt_wesele.Views;
using System.Windows.Input;
using Projekt_wesele.Helpers;

namespace Projekt_wesele.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // Komendy dla przełączania widoków
        public ICommand ShowHomeCommand { get; }
        public ICommand ShowBudgetCommand { get; }
        public ICommand ShowGuestsCommand { get; }
        public ICommand ShowTasksCommand { get; }
        public ICommand ShowEventsCommand { get; }
        public ICommand ToggleNavCommand { get; } // Komenda do ukrywania/rozwijania nawigacji

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private bool _isNavVisible = true;
        public bool IsNavVisible
        {
            get => _isNavVisible;
            set
            {
                _isNavVisible = value;
                OnPropertyChanged(nameof(IsNavVisible));
            }
        }

        public MainViewModel()
        {
            ShowHomeCommand = new RelayCommand(ShowHome);
            ShowBudgetCommand = new RelayCommand(ShowBudget);
            ShowGuestsCommand = new RelayCommand(ShowGuests);
            ShowTasksCommand = new RelayCommand(ShowTasks);
            ShowEventsCommand = new RelayCommand(ShowEvents);
            ToggleNavCommand = new RelayCommand(ToggleNav); // Dodanie komendy

            ShowHome(); // Ustaw domyślny widok
        }

        private void ShowHome()
        {
            CurrentView = new HomeView(this);
            IsNavVisible = true;
        }

        private void ShowBudget()
        {
            CurrentView = new BudgetView();
            IsNavVisible = true;
        }

        private void ShowGuests()
        {
            CurrentView = new GuestsListView();
            IsNavVisible = true;
        }

        private void ShowTasks()
        {
            CurrentView = new TasksListView();
            IsNavVisible = true;
        }

        private void ShowEvents()
        {
            CurrentView = new EventListView();
            IsNavVisible = true;
        }

        private void ToggleNav()
        {
            IsNavVisible = !IsNavVisible; // Zmiana widoczności panelu
        }
    }
}
