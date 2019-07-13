using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TFT_Overlay
{
    public class RadioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.Equals(parameter))
            {
                return true;
            }
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.Equals(true))
            {
                return parameter;
            }
            return Binding.DoNothing;
        }
    }

    public class TabControlRadio : TabControl
    {
        public static readonly DependencyProperty RadioSelectionProperty
            = DependencyProperty.Register("RadioGroup", typeof(string), typeof(TabControlRadio));

        public string RadioSelection
        {
            get { return (string)GetValue(RadioSelectionProperty); }
            set { SetValue(RadioSelectionProperty, value); }
        }
    }

}
