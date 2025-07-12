using System.Windows;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for CoachDashboardView.xaml
    /// </summary>
    public partial class CoachDashboardView : Window
    {
        public CoachDashboardView()
        {
            InitializeComponent();
            ViewModels.CoachChatViewModel viewModel = new ViewModels.CoachChatViewModel(AppSession.CurrentUser.Id);
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }
    }
}
