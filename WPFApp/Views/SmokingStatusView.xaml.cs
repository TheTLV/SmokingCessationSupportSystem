using System.Windows;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for SmokingStatusView.xaml
    /// </summary>
    public partial class SmokingStatusView : Window
    {
        public SmokingStatusView()
        {
            InitializeComponent();

            // Set DataContext để binding với ViewModel
            this.DataContext = new SmokingStatusViewModel();
        }

        private void GoToDashboardButton_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new DashboardView();
            dashboardView.Show();
            this.Close();
        }

        private void NumberOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
