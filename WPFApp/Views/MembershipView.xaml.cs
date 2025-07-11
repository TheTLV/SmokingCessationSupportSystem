using System.Windows;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for MembershipView.xaml
    /// </summary>
    public partial class MembershipView : Window
    {
        public MembershipView()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.MembershipViewModel();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new DashboardView();
            dashboardView.Show();
            this.Close();
        }
    }
}
