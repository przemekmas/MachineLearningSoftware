using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MachineLearningSoftware.Views.Converters
{
    public class ArrayPixelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var position = Int32.Parse(parameter.ToString());
            var array = value as List<int>;
            var newPos = position-1;

            if (array[newPos] == 0)
            {
                return new SolidColorBrush(Colors.White);
            }
            else if (array[newPos] == 1)
            {
                return new SolidColorBrush(Colors.Black);
            }
            else if (array[newPos] == 2)
            {
                return new SolidColorBrush(Colors.Gray);
            }

            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
