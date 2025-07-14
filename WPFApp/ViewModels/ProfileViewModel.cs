using BusinessObjects;
using GalaSoft.MvvmLight;
using Services;
using System.Windows;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly IUserService _userService;

        private string _fullName;
        private string _email;
        private DateTime? _dateOfBirth;

        public string FullName
        {
            get => _fullName;
            set => Set(ref _fullName, value);
        }

        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set => Set(ref _dateOfBirth, value);
        }


        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ProfileViewModel()
        {
            _userService = new UserService();

            LoadUserData();

            SaveCommand = new RelayCommand(SaveProfile);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void LoadUserData()
        {
            var user = _userService.GetUserById(AppSession.CurrentUser.Id);
            if (user != null)
            {
                FullName = user.FullName;
                Email = user.Email;
                DateOfBirth = user.DateOfBirth;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng.");
            }
        }

        private void SaveProfile()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                MessageBox.Show("Vui lòng nhập họ và tên");
                return;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Vui lòng nhập email");
                return;
            }

            if (DateOfBirth == null)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh");
                return;
            }

            _userService.UpdateUser(new User
            {
                Id = AppSession.CurrentUser.Id,
                FullName = FullName,
                Email = Email,
                DateOfBirth = DateOfBirth.Value
            });
            MessageBox.Show("Đã lưu thông tin thành công!");
        }

        private void Cancel()
        {
            LoadUserData();
        }
    }
}