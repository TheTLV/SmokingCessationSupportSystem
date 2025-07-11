using Services;
using System.Windows;

namespace WPFApp.Views
{
    public partial class RegisterView : Window
    {
        private readonly IUserService iUserService;
        public RegisterView()
        {
            InitializeComponent();
            iUserService = new UserService();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.");
                UsernameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Mật khẩu không được để trống.");
                PasswordBox.Focus();
                return;
            }

            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.");
                ConfirmPasswordBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Email không được để trống.");
                EmailTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Họ và tên không được để trống.");
                FullNameTextBox.Focus();
                return;
            }

            if (DateOfBirthDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh!");
                DateOfBirthDatePicker.Focus();
                return;
            }

            if (iUserService.IsUsernameExists(UsernameTextBox.Text))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại.");
                UsernameTextBox.Focus();
                return;
            }

            if (iUserService.IsEmailExists(EmailTextBox.Text))
            {
                MessageBox.Show("Email đã được sử dụng.");
                EmailTextBox.Focus();
                return;
            }

            bool result = iUserService.AddUser(
                UsernameTextBox.Text,
                PasswordBox.Password,
                EmailTextBox.Text,
                FullNameTextBox.Text,
                DateOfBirthDatePicker.SelectedDate.Value
            );

            if (result)
                MessageBox.Show("Đăng ký thành công!");
            else
                MessageBox.Show("Đăng ký thất bại!");
        }
    }
}
