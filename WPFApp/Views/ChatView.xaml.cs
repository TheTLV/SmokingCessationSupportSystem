using System.Windows;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : Window
    {
        public ChatView()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Đóng cửa sổ chat hiện tại
            this.Close();

            // Focus vào cửa sổ CoachList nếu nó tồn tại
            if (AppSession.CurrentCoachListView != null && AppSession.CurrentCoachListView.IsLoaded)
            {
                AppSession.CurrentCoachListView.Activate();
                AppSession.CurrentCoachListView.Focus();
            }
        }
    }
}
