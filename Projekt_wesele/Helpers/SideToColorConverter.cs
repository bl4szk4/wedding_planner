using Projekt_wesele.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Projekt_wesele.Helpers
{
    public class SideToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GuestSide side)
            {
                switch (side)
                {
                    case GuestSide.Bride:
                        return Brushes.Pink;
                    case GuestSide.Groom:
                        return Brushes.LightBlue;
                    default:
                        return Brushes.Gray;
                }
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
