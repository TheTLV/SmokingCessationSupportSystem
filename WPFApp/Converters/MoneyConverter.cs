using System.Globalization;
using System.Windows.Data;

namespace WPFApp.Converters
{
    public class MoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal money)
            {
                // Định dạng số với dấu phân cách hàng nghìn và thêm "VNĐ"
                return money.ToString("N0") + " VNĐ";
            }
            return "0 VNĐ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}