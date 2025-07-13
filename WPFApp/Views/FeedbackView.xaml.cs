using System.Windows;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for FeedbackView.xaml
    /// </summary>
    public partial class FeedbackView : Window
    {
        public FeedbackView()
        {
            InitializeComponent();
            DataContext = new WPFApp.ViewModels.FeedbackViewModel();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new DashboardView();
            dashboardView.Show();
            this.Close();
        }
    }
}
