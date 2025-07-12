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
            this.DataContext = new WPFApp.ViewModels.DashboardViewModel();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (DataContext is WPFApp.ViewModels.DashboardViewModel vm)
            {
                vm.GetType().GetMethod("LoadNotifications", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?.Invoke(vm, null);
            }
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

        private void GoToCoachListButton_Click(object sender, RoutedEventArgs e)
        {
            var coachChatView = new CoachListView();
            coachChatView.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        private void GoToAIChatSupportButton_Click(object sender, RoutedEventArgs e)
        {
            var aiChatSupportView = new AIChatSupportView();
            aiChatSupportView.Show();
            this.Close();
        }
    }
}
