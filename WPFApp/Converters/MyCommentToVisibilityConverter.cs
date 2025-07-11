using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFApp.Converters
{
    public class MyCommentToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int userId)
            {
                // AppSession.CurrentUser.Id là static property
                return userId == AppSession.CurrentUser.Id ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}