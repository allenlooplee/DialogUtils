using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DialogUtils.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        private readonly Visibility _trueValue;
        private readonly Visibility _falseValue;

        public static BooleanToVisibilityConverter TrueToVisibleConverter { get; } = new BooleanToVisibilityConverter(Visibility.Visible, Visibility.Collapsed);
        public static BooleanToVisibilityConverter TrueToCollapsedConverter { get; } = new BooleanToVisibilityConverter(Visibility.Collapsed, Visibility.Visible);

        public BooleanToVisibilityConverter(Visibility trueValue, Visibility falseValue)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolean)
            {
                return boolean ? _trueValue : _falseValue;
            }
            else
            {
                return _falseValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
