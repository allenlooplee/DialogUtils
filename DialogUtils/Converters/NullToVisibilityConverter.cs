using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DialogUtils.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        private readonly Visibility _nullValue;
        private readonly Visibility _notNullValue;

        public static NullToVisibilityConverter NullToCollapsedConverter { get; } = new NullToVisibilityConverter(Visibility.Collapsed, Visibility.Visible);
        public static NullToVisibilityConverter NullToVisibleConverter { get; } = new NullToVisibilityConverter(Visibility.Visible, Visibility.Collapsed);

        public NullToVisibilityConverter(Visibility nullValue, Visibility notNullValue)
        {
            _nullValue = nullValue;
            _notNullValue = notNullValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? _nullValue : _notNullValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
