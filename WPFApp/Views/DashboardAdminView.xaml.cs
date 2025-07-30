using System.Windows;
using System.Windows.Controls;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for DashboardAdminView.xaml
    /// </summary>
    public partial class DashboardAdminView : Window
    {
        public DashboardAdminView()
        {
            InitializeComponent();
            DataContext = new ViewModels.DashboardAdminViewModel();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        private void ShowAccountManagement_Click(object sender, RoutedEventArgs e)
        {
            ShowPopup("QUẢN LÝ TÀI KHOẢN", new AccountManagementView());
        }

        private void ShowPopup(string title, UserControl content)
        {
            PopupTitle.Text = title;
            PopupBody.Content = content;
            PopupContainer.Visibility = Visibility.Visible;
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            PopupContainer.Visibility = Visibility.Collapsed;
            PopupBody.Content = null;
        }
    }
}
