using BusinessObjects;
using GalaSoft.MvvmLight;
using Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class DashboardAdminViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IFeedbackService _feedbackService;

        public ObservableCollection<User> Users { get; } = new();
        public ObservableCollection<FeedbackSystem> Feedbacks { get; } = new();

        public ICommand DeleteUserCommand { get; }
        public ICommand DeleteFeedbackCommand { get; }

        public DashboardAdminViewModel()
        {
            _userService = new UserService();
            _feedbackService = new FeedbackService();

            DeleteUserCommand = new RelayCommand(obj => DeleteUser((User)obj));
            DeleteFeedbackCommand = new RelayCommand(obj => DeleteFeedback((FeedbackSystem)obj));

            LoadData();
        }

        private void LoadData()
        {
            List<User> users = _userService.GetUsersForManagement();
            foreach (var user in users)
            {
                Users.Add(user);
            }

            List<Feedback> feedbacks = _feedbackService.GetAllFeedbacks();
            foreach (var feedback in feedbacks)
            {
                FeedbackSystem feedbackSystem = new FeedbackSystem
                {
                    Id = feedback.Id,
                    UserId = feedback.UserId,
                    Content = feedback.Content,
                    Rating = feedback.Rating,
                    CreatedAt = feedback.CreatedAt,
                };
                Feedbacks.Add(feedbackSystem);
            }
        }

        private void DeleteUser(User user)
        {
            if (MessageBox.Show($"Xóa tài khoản {user.FullName}?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _userService.DeleteUser(user.Id);
                Users.Remove(user);
                MessageBox.Show($"Đã xóa tài khoản {user.FullName} thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteFeedback(FeedbackSystem feedback)
        {
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa không?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _feedbackService.DeleteFeedback(feedback.Id);
                Feedbacks.Remove(feedback);
                MessageBox.Show("Đã xóa phản hồi thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

    public class FeedbackSystem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; } = null!;

        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public string FeedbackType
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Content)) return "";
                var parts = Content.Split(new[] { ':' }, 2);
                return parts.Length > 1 ? parts[0].Trim() : "";
            }
        }
        public string FeedbackContent
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Content)) return "";
                var parts = Content.Split(new[] { ':' }, 2);
                return parts.Length > 1 ? parts[1].Trim() : Content;
            }
        }
    }
}