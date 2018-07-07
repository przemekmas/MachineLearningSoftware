using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MachineLearningSoftware.Views.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = Visibility.Collapsed;
            if (value is bool isVisible && isVisible)
            {
                visibility = Visibility.Visible;
            }
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
