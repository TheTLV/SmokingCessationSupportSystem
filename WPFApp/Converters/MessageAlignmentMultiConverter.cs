using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFApp.Converters
{
    public class MessageAlignmentMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && values[0] is int senderId && values[1] is int currentUserId)
            {
                return senderId == currentUserId ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            }
            return HorizontalAlignment.Left;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MessageBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int senderId && parameter is int currentUserId)
            {
                return senderId == currentUserId ? "#1976D2" : "#E3F2FD";
            }
            return "#E3F2FD";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MessageForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int senderId && parameter is int currentUserId)
            {
                return senderId == currentUserId ? "White" : "Black";
            }
            return "Black";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
