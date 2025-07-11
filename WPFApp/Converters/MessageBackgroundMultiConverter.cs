using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFApp.Converters
{
    public class MessageBackgroundMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && values[0] is int senderId && values[1] is int currentUserId)
            {
                return senderId == currentUserId ? (Brush)new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1976D2")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3F2FD"));
            }
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3F2FD"));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}