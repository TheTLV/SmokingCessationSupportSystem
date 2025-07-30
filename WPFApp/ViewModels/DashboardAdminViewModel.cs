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

        public ObservableCollection<User> Users { get; } = new();

        public ICommand DeleteUserCommand { get; }
        public ICommand DeleteFeedbackCommand { get; }

        public DashboardAdminViewModel()
        {
            _userService = new UserService();

            DeleteUserCommand = new RelayCommand(obj => DeleteUser((User)obj));

            LoadData();
        }

        private void LoadData()
        {
            List<User> users = _userService.GetUsersForManagement();
            foreach (var user in users)
            {
                Users.Add(user);
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
    }
}