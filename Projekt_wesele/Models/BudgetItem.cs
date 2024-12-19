using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Projekt_wesele.Models
{
    public class BudgetItem : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private decimal _cost;
        private BudgetItemCategory _category = BudgetItemCategory.Other;
        private bool _isEssential = false;
        private bool _isPayed = false;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public decimal Cost
        {
            get => _cost;
            set { _cost = value; OnPropertyChanged(); }
        }

        public bool IsEssential
        {
            get => _isEssential;
            set { _isEssential = value; OnPropertyChanged(); }
        }

        public bool IsPayed
        {
            get => _isPayed;
            set { _isPayed = value; OnPropertyChanged(); }
        }

        public BudgetItemCategory Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum BudgetItemCategory
    {
        Decorations = 0,
        Hall = 1,
        Clothes = 2,
        Food = 3,
        Sweets = 4,
        Drinks = 5,
        Services = 6,
        Other = 7
    }
}
