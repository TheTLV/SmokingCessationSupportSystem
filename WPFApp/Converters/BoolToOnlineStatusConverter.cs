using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace WPFApp.Converters
{
    class BoolToOnlineStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Brushes.LimeGreen : Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
