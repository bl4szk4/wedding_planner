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

        // Aktualnie wyświetlany widok
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


        // Kontrola widoczności przycisku "Home"
        private bool _isHomeVisible;
        public bool IsHomeVisible
        {
            get => _isHomeVisible;
            set
            {
                _isHomeVisible = value;
                OnPropertyChanged(nameof(IsHomeVisible));
            }
        }


        // Konstruktor
        public MainViewModel()
        {
            // Inicjalizacja komend
            ShowHomeCommand = new RelayCommand(ShowHome);
            ShowBudgetCommand = new RelayCommand(ShowBudget);
            ShowGuestsCommand = new RelayCommand(ShowGuests);
            ShowTasksCommand = new RelayCommand(ShowTasks);
            ShowEventsCommand = new RelayCommand(ShowEvents);

            // Domyślnie wyświetlany jest widok główny
            ShowHome();
        }

        // Przełączenie na widok główny (HomeView)
        private void ShowHome()
        {
            CurrentView = new HomeView(this); // Widok główny z przyciskami
            IsHomeVisible = false; // Ukryj przycisk Home na ekranie głównym
        }

        // Przełączenie na widok Budżetu
        private void ShowBudget()
        {
            CurrentView = new BudgetView();
            IsHomeVisible = true; // Pokaż przycisk Home
        }

        // Przełączenie na widok Gości
        private void ShowGuests()
        {
            CurrentView = new GuestsListView();
            IsHomeVisible = true;
        }

        // Przełączenie na widok Zadań
        private void ShowTasks()
        {
            CurrentView = new TasksListView();
            IsHomeVisible = true;
        }

        // Przełączenie na widok Wydarzeń
        private void ShowEvents()
        {
            CurrentView = new EventListView();
            IsHomeVisible = true;
        }
    }
}
