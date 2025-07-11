using System.Windows;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for NotificationView.xaml
    /// </summary>
    public partial class NotificationView : Window
    {
        public NotificationView()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.NotificationViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var DashboardView = new Views.DashboardView();
            DashboardView.Show();
            this.Close();
        }
    }
}
