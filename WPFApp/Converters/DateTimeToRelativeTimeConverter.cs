using System.Globalization;
using System.Windows.Data;

namespace WPFApp.Converters
{
    public class DateTimeToRelativeTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                var span = DateTime.Now - dateTime;
                if (span.TotalDays > 1)
                    return $"{(int)span.TotalDays} ngày trước";
                if (span.TotalHours > 1)
                    return $"{(int)span.TotalHours} giờ trước";
                if (span.TotalMinutes > 1)
                    return $"{(int)span.TotalMinutes} phút trước";
                return "Vừa xong";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}