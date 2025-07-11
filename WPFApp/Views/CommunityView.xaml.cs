using System.Windows;
using System.Windows.Controls;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for CommunityView.xaml
    /// </summary>
    public partial class CommunityView : Window
    {
        public CommunityView()
        {
            InitializeComponent();
            DataContext = new CommunityViewModel();
        }

        private void PostMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.ContextMenu != null)
            {
                button.ContextMenu.DataContext = button.DataContext;
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }

        private void CommentMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.ContextMenu != null)
            {
                button.ContextMenu.DataContext = button.Tag; // Tag là comment
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }

        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new DashboardView();
            dashboardView.Show();
            this.Close();
        }
    }
}
