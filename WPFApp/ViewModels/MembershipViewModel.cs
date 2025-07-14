using BusinessObjects;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class MembershipViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MembershipPackage> MembershipPackages { get; set; }

        private MembershipPackage? _selectedPackage;
        public MembershipPackage? SelectedPackage
        {
            get => _selectedPackage;
            set
            {
                _selectedPackage = value;
                OnPropertyChanged(nameof(SelectedPackage));
            }
        }

        private MembershipPackage _currentPackage;
        public MembershipPackage CurrentPackage
        {
            get => _currentPackage;
            set
            {
                _currentPackage = value;
                OnPropertyChanged(nameof(CurrentPackage));
            }
        }

        public ICommand SelectPackageCommand { get; }
        public ICommand ConfirmPaymentCommand { get; }
        public ICommand DeleteCurrentPackageCommand { get; }

        private readonly IMembershipService _membershipService;

        public MembershipViewModel()
        {
            MembershipPackages = new ObservableCollection<MembershipPackage>();
            SelectPackageCommand = new RelayCommand(obj => SelectPackage((MembershipPackage)obj));
            ConfirmPaymentCommand = new RelayCommand(obj => ConfirmPayment(), obj => CanConfirmPayment());
            DeleteCurrentPackageCommand = new RelayCommand(obj => DeleteCurrentPackage(), obj => CanDeleteCurrentPackage());

            _membershipService = new MembershipService();

            LoadPackages();
            LoadCurrentPackage();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadPackages()
        {
            var packages = new List<MembershipPackage>
            {
                new MembershipPackage
                {
                    PackageName = "Basic Membership",
                    Price = 100000,
                    DurationMonths = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(1),
                    Description = "Được sử dụng tính năng chat với coach trong 1 tháng."
                },
                new MembershipPackage
                {
                    PackageName = "Premium Membership",
                    Price = 300000,
                    DurationMonths = 3,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(3),
                    Description = "Được sử dụng tính năng chat với coach trong 3 tháng."
                },
                new MembershipPackage
                {
                    PackageName = "Family Plan",
                    Price = 500000,
                    DurationMonths = 6,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(6),
                    Description = "Được sử dụng tính năng chat với coach trong 6 tháng."
                }
            };

            MembershipPackages.Clear();
            foreach (var package in packages)
            {
                MembershipPackages.Add(package);
            }
        }

        private void LoadCurrentPackage()
        {
            var userId = AppSession.CurrentUser?.Id ?? 0;
            var currentPackage = _membershipService.GetCurrentMembership(userId);
            if (currentPackage != null)
            {
                CurrentPackage = new MembershipPackage
                {
                    Id = currentPackage.Id,
                    PackageName = currentPackage.PackageName,
                    Price = currentPackage.Price,
                    DurationMonths = (int)((currentPackage.EndDate - currentPackage.StartDate).TotalDays / 30),
                    StartDate = currentPackage.StartDate,
                    EndDate = currentPackage.EndDate,
                    Description = "Your current active membership package."
                };
            }
        }

        private void SelectPackage(MembershipPackage package)
        {
            SelectedPackage = package;
        }

        private void ConfirmPayment()
        {
            if (SelectedPackage != null)
            {
                try
                {
                    var membership = new Membership
                    {
                        PackageName = SelectedPackage.PackageName,
                        Price = SelectedPackage.Price,
                        StartDate = SelectedPackage.StartDate,
                        EndDate = SelectedPackage.EndDate,
                        UserId = AppSession.CurrentUser?.Id ?? 0
                    };

                    bool isExisting = _membershipService.IsExistingPackage(membership.UserId);
                    if (isExisting)
                    {
                        System.Windows.MessageBox.Show("Bạn đã có gói thành viên hiện tại. Vui lòng hủy gói cũ trước khi đăng ký gói mới.", "Thông báo",
                            System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                        return;
                    }
                    bool success = _membershipService.AddMembershipPackage(membership);

                    if (success)
                    {
                        System.Windows.MessageBox.Show("Đăng ký gói thành viên thành công!", "Thành công",
                            System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Có lỗi xảy ra khi đăng ký gói thành viên!", "Lỗi",
                            System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
        }

        private bool CanConfirmPayment()
        {
            return SelectedPackage != null;
        }

        private void DeleteCurrentPackage()
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa gói hiện tại?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _membershipService.DeleteMembershipPackage(new Membership
                {
                    Id = CurrentPackage.Id
                });
                CurrentPackage = null;
                MessageBox.Show("Đã xóa gói thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool CanDeleteCurrentPackage()
        {
            return CurrentPackage != null;
        }

        public class MembershipPackage
        {
            public int Id { get; set; }
            public string PackageName { get; set; }
            public decimal Price { get; set; }
            public int DurationMonths { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Description { get; set; }
        }
    }
}