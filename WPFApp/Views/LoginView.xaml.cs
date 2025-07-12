using BusinessObjects;
using Services;
using System.Windows;

namespace WPFApp.Views
{
    public partial class LoginView : Window
    {
        private readonly IUserService iUserService;
        private readonly INotificationService iNotificationService;
        public LoginView()
        {
            InitializeComponent();
            iUserService = new UserService();
            iNotificationService = new NotificationService();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerView = new RegisterView();
            registerView.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                MessageBox.Show("Username cannot be empty.");
                UsernameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Password cannot be empty.");
                PasswordBox.Focus();
                return;
            }

            User user = iUserService.GetUserByNameAndPassword(UsernameTextBox.Text, PasswordBox.Password);
            if (user != null)
            {
                if (user.Role == "User")
                {
                    MessageBox.Show("Login Success!");
                    AppSession.CurrentUser = user;
                    iNotificationService.SendDailyNotificationForUser(user.Id);
                    this.Hide();
                    DashboardView dashboardView = new DashboardView();
                    dashboardView.Show();
                }
                else if (user.Role == "Coach")
                {
                    MessageBox.Show("Login Success!");
                    AppSession.CurrentUser = user;
                    this.Hide();
                    CoachDashboardView coachDashboardView = new CoachDashboardView();
                    coachDashboardView.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}
