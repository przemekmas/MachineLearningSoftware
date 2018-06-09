using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MachineLearningSoftware.Views.Converters
{
    public class BooleanInvertedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (bool)value;

            if (visibility)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
