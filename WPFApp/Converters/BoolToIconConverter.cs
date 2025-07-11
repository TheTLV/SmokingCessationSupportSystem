using System.Globalization;
using System.Windows.Data;

namespace WPFApp.Converters
{
    public class BoolToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return b ? "ThumbUp" : "ThumbUpOutline";
            return "ThumbUpOutline";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}