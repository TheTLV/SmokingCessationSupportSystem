using System.Windows;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for QuitPlanView.xaml
    /// </summary>
    public partial class QuitPlanView : Window
    {
        public QuitPlanView()
        {
            InitializeComponent();

            // Set DataContext để binding với ViewModel
            this.DataContext = new QuitPlanViewModel();
        }

        private void GoToDashboardButton_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new DashboardView();
            dashboardView.Show();
            this.Close();
        }

        private void CreatePlanButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as QuitPlanViewModel;
            if (viewModel != null)
            {
                viewModel.CreateQuitPlan();
            }
        }
    }
}
