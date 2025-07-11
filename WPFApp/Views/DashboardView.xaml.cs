using System.Windows;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        private void GoToSmokingStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var smokingStatusView = new SmokingStatusView();
            smokingStatusView.Show();
            this.Close();
        }

        private void GoToQuitPlanButton_Click(object sender, RoutedEventArgs e)
        {
            var quitPlanView = new QuitPlanView();
            quitPlanView.Show();
            this.Close();
        }

        private void GoToMembershipButton_Click(object sender, RoutedEventArgs e)
        {
            var membershipView = new MembershipView();
            membershipView.Show();
            this.Close();
        }

        private void NotificationBell_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            var notificationView = new NotificationView();
            notificationView.Show();
            this.Close();
        }

        private void GoToAchievementButton_Click(object sender, RoutedEventArgs e)
        {
            var achievementView = new AchievementView();
            achievementView.Show();
            this.Close();
        }

        private void GoToCommunityButton_Click(object sender, RoutedEventArgs e)
        {
            var communityView = new CommunityView();
            communityView.Show();
            this.Close();
        }

        private void GoToCoachChatButton_Click(object sender, RoutedEventArgs e)
        {
            var coachChatView = new CoachListView();
            coachChatView.Show();
            this.Close();
        }
    }
}
