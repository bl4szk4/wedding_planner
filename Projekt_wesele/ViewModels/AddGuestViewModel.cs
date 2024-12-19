using Projekt_wesele.Helpers;
using Projekt_wesele.Models;
using System.Windows.Input;

namespace Projekt_wesele.ViewModels
{
    public class AddGuestViewModel : DialogViewModelBase
    {
        public Guest Guest { get; set; }

        public ICommand SetSideCommand { get; }

        public bool IsBrideSelected
        {
            get => Guest.Side == GuestSide.Bride;
            set
            {
                if (value)
                {
                    Guest.Side = GuestSide.Bride;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsGroomSelected));
                }
            }
        }

        public bool IsGroomSelected
        {
            get => Guest.Side == GuestSide.Groom;
            set
            {
                if (value)
                {
                    Guest.Side = GuestSide.Groom;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsBrideSelected));
                }
            }
        }

        public AddGuestViewModel(Guest guest)
        {
            Guest = guest;
            SetSideCommand = new RelayCommand<GuestSide>(SetSide);
        }

        private void SetSide(GuestSide side)
        {
            Guest.Side = side;
            OnPropertyChanged(nameof(IsBrideSelected));
            OnPropertyChanged(nameof(IsGroomSelected));
        }

        protected override void Save()
        {
            // Logika walidacji przed zapisem (jeśli potrzebna)
            base.Save();
        }
    }
}
