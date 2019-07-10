using System;
using System.Globalization;
using System.Windows.Data;

namespace TFT_Overlay.Utilities
{
    public class IsEqualOrGreaterThanConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsEqualOrGreaterThanConverter();

        #region Methods...

        #region Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int.TryParse(value.ToString(), out int intValue);
            int.TryParse(parameter.ToString(), out int compareToValue);

            return intValue >= compareToValue;
        }
        #endregion

        #region ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion 

        #endregion
    }
}
