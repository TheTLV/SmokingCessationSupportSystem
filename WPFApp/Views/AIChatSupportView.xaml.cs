using System.Windows;
using System.Windows.Input;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for AIChatSupportView.xaml
    /// </summary>
    public partial class AIChatSupportView : Window
    {
        public AIChatSupportView()
        {
            InitializeComponent();
            DataContext = new ViewModels.AIChatSupportViewModel();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var viewModel = DataContext as ViewModels.AIChatSupportViewModel;
                if (viewModel?.SendCommand?.CanExecute(null) == true)
                {
                    viewModel.SendCommand.Execute(null);
                }
                e.Handled = true;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardView dashboardView = new DashboardView();
            dashboardView.Show();
            this.Close();
        }
    }
}
