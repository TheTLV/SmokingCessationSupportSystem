using BusinessObjects;
using System.Windows;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CoachListView : Window
    {
        private CoachListViewModel _viewModel;
        public CoachListView()
        {
            InitializeComponent();
            _viewModel = new ViewModels.CoachListViewModel();
            _viewModel.OpenChatAction = OpenChatDialog;
            DataContext = _viewModel;

            // Lưu reference đến cửa sổ hiện tại
            AppSession.CurrentCoachListView = this;

            // Đăng ký event để reset reference khi cửa sổ đóng
            this.Closed += (s, e) => AppSession.CurrentCoachListView = null;
        }

        private void OpenChatDialog(Coach selectedCoach)
        {
            int currentUserId = AppSession.CurrentUser.Id;
            var chatView = new ChatView
            {
                Owner = this,
                DataContext = new ChatViewModel(currentUserId, selectedCoach)
            };
            chatView.Show(); // Sử dụng Show() thay vì ShowDialog() để cho phép tương tác với cả hai cửa sổ
        }

        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboard = new DashboardView();
            dashboard.Show();
            this.Close();
        }
    }
}
