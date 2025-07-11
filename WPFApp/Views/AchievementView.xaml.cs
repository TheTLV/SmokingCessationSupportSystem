using System.Windows;

namespace WPFApp.Views
{
    public partial class AchievementView : Window
    {
        public AchievementView()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.AchievementViewModel();
        }

        private void BackToDashboardButton_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new DashboardView();
            dashboardView.Show();
            this.Close();
        }
    }
}
