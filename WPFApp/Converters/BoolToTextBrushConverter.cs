using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace WPFApp.Converters
{
    public class BoolToTextBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool && (bool)value) ?
                Brushes.White : // Chữ trắng cho tin nhắn người dùng
                Brushes.Black; // Chữ đen cho tin nhắn AI
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
