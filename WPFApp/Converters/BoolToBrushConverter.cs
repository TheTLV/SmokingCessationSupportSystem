using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFApp.Converters
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BoolToBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; } = new SolidColorBrush(Color.FromRgb(25, 118, 210)); // Xanh đậm cho người dùng
        public Brush FalseBrush { get; set; } = new SolidColorBrush(Color.FromRgb(240, 248, 255)); // Xanh nhạt cho AI

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? TrueBrush : FalseBrush;
            }
            return FalseBrush; // Default value if input is not boolean
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}