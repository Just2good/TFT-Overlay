using System;
using System.Globalization;
using System.Windows.Data;

namespace TFT_Overlay.Utilities
{
    public class IsEqualOrGreaterThanConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsEqualOrGreaterThanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int.TryParse(value.ToString(), out int intValue);
            int.TryParse(parameter.ToString(), out int compareToValue);

            return intValue >= compareToValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
