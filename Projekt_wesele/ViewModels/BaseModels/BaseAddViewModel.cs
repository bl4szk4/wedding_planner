using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Projekt_wesele.Helpers;

namespace Projekt_wesele.ViewModels
{
    public abstract class DialogViewModelBase : INotifyPropertyChanged
    {
        public event EventHandler RequestClose;
        public bool DialogResult { get; protected set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        protected DialogViewModelBase()
        {
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        protected virtual void Save()
        {
            DialogResult = true;
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void Cancel()
        {
            DialogResult = false;
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
