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


        public MainViewModel()
        {
            ShowHomeCommand = new RelayCommand(ShowHome);
            ShowBudgetCommand = new RelayCommand(ShowBudget);
            ShowGuestsCommand = new RelayCommand(ShowGuests);
            ShowTasksCommand = new RelayCommand(ShowTasks);
            ShowEventsCommand = new RelayCommand(ShowEvents);

            ShowHome();
        }

        private void ShowHome()
        {
            CurrentView = new HomeView(this);
            IsHomeVisible = false;
        }

        private void ShowBudget()
        {
            CurrentView = new BudgetView();
            IsHomeVisible = true;
        }

        private void ShowGuests()
        {
            CurrentView = new GuestsListView();
            IsHomeVisible = true;
        }

        private void ShowTasks()
        {
            CurrentView = new TasksListView();
            IsHomeVisible = true;
        }

        private void ShowEvents()
        {
            CurrentView = new EventListView();
            IsHomeVisible = true;
        }
    }
}
