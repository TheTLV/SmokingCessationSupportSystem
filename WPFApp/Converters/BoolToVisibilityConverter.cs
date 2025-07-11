using System.Globalization;
using System.Windows.Data;

namespace WPFApp.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
            return System.Windows.Visibility.Collapsed; // Default to Collapsed if value is not a boolean
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Windows.Visibility visibility)
            {
                return visibility == System.Windows.Visibility.Visible;
            }
            return false; // Default to false if value is not a Visibility
        }
    }
}
