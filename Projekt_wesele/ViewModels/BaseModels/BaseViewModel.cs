using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Projekt_wesele.Helpers;
using System.Diagnostics;


namespace Projekt_wesele.ViewModels
{
    public abstract class ListViewModelBase<T> : ViewModelBase where T : class, INotifyPropertyChanged
    {
        protected readonly DbContext _context;

        public ObservableCollection<T> Items { get; set; }
        protected List<T> _allItems;

        public ICommand ApplyFiltersCommand { get; }
        public ICommand ClearFiltersCommand { get; }
        public ICommand AddItemCommand { get; }
        public ICommand DeleteItemCommand { get; }

        protected ListViewModelBase(DbContext context)
        {
            _context = context;
            LoadItems();

            ApplyFiltersCommand = new RelayCommand(ApplyFilters);
            ClearFiltersCommand = new RelayCommand(ClearFilters);
            AddItemCommand = new RelayCommand(AddItem);
            DeleteItemCommand = new RelayCommand<T>(DeleteItem);
        }

        protected virtual void LoadItems()
        {
            var itemsFromDb = _context.Set<T>().ToList();
            Debug.WriteLine($"Loaded {itemsFromDb.Count} items from database."); // Logowanie liczby elementów

            Items = new ObservableCollection<T>(itemsFromDb);

            foreach (var item in Items)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }

            Items.CollectionChanged += Items_CollectionChanged;

            OnPropertyChanged(nameof(Items));
        }

        protected virtual void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems.Cast<T>())
                {
                    if (_context.Entry(newItem).State == EntityState.Detached)
                    {
                        _context.Set<T>().Add(newItem);
                    }
                    newItem.PropertyChanged += Item_PropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var removedItem in e.OldItems.Cast<T>())
                {
                    _context.Set<T>().Remove(removedItem);
                    removedItem.PropertyChanged -= Item_PropertyChanged;
                }
            }
        }

        protected virtual async void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is T updatedItem)
            {
                var entry = _context.Entry(updatedItem);

                if (entry.State == EntityState.Unchanged)
                {
                    entry.State = EntityState.Modified;
                }

                await Task.Delay(300);
                _context.SaveChanges();
            }
        }

        public abstract void ApplyFilters();

        public virtual void ClearFilters()
        {
            LoadItems();
        }

        public abstract void AddItem();

        public virtual void DeleteItem(T item)
        {
            if (item != null)
            {
                Items.Remove(item);
                _context.Set<T>().Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
