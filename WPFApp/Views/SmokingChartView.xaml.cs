using System.Windows;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for SmokingChartView.xaml
    /// </summary>
    public partial class SmokingChartView : Window
    {
        public SmokingChartView(IEnumerable<BusinessObjects.SmokingStatus> data)
        {
            InitializeComponent();
            DataContext = new SmokingChartViewModel(data);
        }
    }
}
