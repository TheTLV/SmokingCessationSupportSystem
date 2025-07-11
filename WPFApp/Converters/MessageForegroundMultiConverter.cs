using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFApp.Converters
{
    public class MessageForegroundMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && values[0] is int senderId && values[1] is int currentUserId)
            {
                return senderId == currentUserId ? Brushes.White : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222222"));
            }
            return Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}