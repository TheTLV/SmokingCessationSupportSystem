using System.Windows;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Window
    {
        public ProfileView()
        {
            InitializeComponent();
            DataContext = new ViewModels.ProfileViewModel();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardView dashboardView = new DashboardView();
            dashboardView.Show();
            this.Close();
        }
    }
}
