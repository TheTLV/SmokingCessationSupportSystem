using Services;
using System.Windows;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        private readonly IMembershipService membershipService;

        public DashboardView()
        {
            InitializeComponent();
            this.DataContext = new WPFApp.ViewModels.DashboardViewModel();
            membershipService = new MembershipService();
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

        private void GoToCommunityButton_Click(object sender, RoutedEventArgs e)
        {
            var communityView = new CommunityView();
            communityView.Show();
            this.Close();
        }

        private void GoToCoachListButton_Click(object sender, RoutedEventArgs e)
        {
            var currentPackage = membershipService.GetCurrentMembership(AppSession.CurrentUser.Id);
            if (currentPackage == null)
            {
                MessageBox.Show("Bạn cần nâng cấp tài khoản để có thể sử dụng tính năng này.");
                return;
            }
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

        private void GoToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            ProfileView profileView = new ProfileView();
            profileView.Show();
            this.Close();
        }
    }
}
