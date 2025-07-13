using BusinessObjects;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPFApp.Views;
using System.Linq;

namespace WPFApp.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly INotificationService _notificationService;
        private readonly IQuitPlanService _quitPlanService = new QuitPlanService();
        private readonly ISmokingStatusService _smokingStatusService = new SmokingStatusService();

        private ObservableCollection<Notification> _notifications;
        public ObservableCollection<Notification> Notifications
        {
            get => _notifications;
            set
            {
                _notifications = value;
                OnPropertyChanged(nameof(Notifications));
            }
        }

        private int _unreadNotificationCount;
        public int UnreadNotificationCount
        {
            get => _unreadNotificationCount;
            set
            {
                _unreadNotificationCount = value;
                OnPropertyChanged(nameof(UnreadNotificationCount));
                OnPropertyChanged(nameof(HasUnreadNotifications));
            }
        }

        public bool HasUnreadNotifications => UnreadNotificationCount > 0;

        private int _totalQuitDays;
        public int TotalQuitDays
        {
            get => _totalQuitDays;
            set
            {
                _totalQuitDays = value;
                OnPropertyChanged(nameof(TotalQuitDays));
            }
        }

        private decimal _moneySaved;
        public decimal MoneySaved
        {
            get => _moneySaved;
            set
            {
                _moneySaved = value;
                OnPropertyChanged(nameof(MoneySaved));
            }
        }

        public ICommand ViewAllNotificationsCommand { get; }

        public class CurrentPlanDashboardModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public double Progress { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
        private CurrentPlanDashboardModel _currentPlanDashboard;
        public CurrentPlanDashboardModel CurrentPlanDashboard
        {
            get => _currentPlanDashboard;
            set { _currentPlanDashboard = value; OnPropertyChanged(nameof(CurrentPlanDashboard)); }
        }
        public class JournalRecord
        {
            public DateTime Date { get; set; }
            public int Cigarettes { get; set; }
            public int Packs { get; set; }
            public decimal CostPerPack { get; set; }
            public decimal WeeklyCost { get; set; }
            public decimal MonthlyCost { get; set; }
            public int TotalCigarettes { get; set; }
        }
        private ObservableCollection<JournalRecord> _recentJournals = new ObservableCollection<JournalRecord>();
        public ObservableCollection<JournalRecord> RecentJournals
        {
            get => _recentJournals;
            set { _recentJournals = value; OnPropertyChanged(nameof(RecentJournals)); }
        }

        private void LoadNotifications()
        {
            var userId = AppSession.CurrentUser?.Id ?? 0;
            if (userId == 0)
            {
                Notifications = new ObservableCollection<Notification>();
                UnreadNotificationCount = 0;
                return;
            }
            var allNoti = _notificationService.GetAllNotificationsByUserId(userId);
            Notifications = new ObservableCollection<Notification>(allNoti);
            var unreadCount = allNoti.Count(n => !n.IsRead);
            UnreadNotificationCount = unreadCount;
        }

        private void LoadQuitStats()
        {
            var userId = AppSession.CurrentUser?.Id ?? 0;
            if (userId == 0) return;
            // Tính tổng số ngày không hút thuốc từ tất cả QuitPlan
            var plans = _quitPlanService.GetAllQuitPlansByUserId(userId);
            int totalDays = 0;
            foreach (var plan in plans)
            {
                int days = (plan.TargetDate - plan.StartDate).Days;
                if (days > 0) totalDays += days;
            }
            TotalQuitDays = totalDays;

            // Lấy SmokingStatus mới nhất để tính tiền tiết kiệm
            var statuses = _smokingStatusService.GetSmokingStatusesByUserId(userId);
            var latest = statuses.OrderByDescending(s => s.RecordDate).FirstOrDefault();
            if (latest != null)
            {
                // Tiền tiết kiệm = tổng số ngày không hút * (chi phí/ngày)
                // chi phí/ngày = (PacksPerWeek * CostPerPack) / 7
                decimal dailyCost = (latest.PacksPerWeek * latest.CostPerPack) / 7;
                MoneySaved = dailyCost * TotalQuitDays;
            }
            else
            {
                MoneySaved = 0;
            }
        }

        private void LoadCurrentPlanAndJournals()
        {
            var userId = AppSession.CurrentUser?.Id ?? 0;
            if (userId == 0) return;
            // Kế hoạch hiện tại
            var plan = _quitPlanService.GetCurrentQuitPlanById(userId);
            if (plan != null)
            {
                var totalDays = (plan.TargetDate - plan.StartDate).Days;
                var completedDays = (DateTime.Today - plan.StartDate).Days;
                double progress = totalDays > 0 ? Math.Min(100, Math.Max(0, (double)completedDays / totalDays * 100)) : 0;
                CurrentPlanDashboard = new CurrentPlanDashboardModel
                {
                    Name = plan.Reason,
                    Description = plan.Stages,
                    Progress = progress,
                    StartDate = plan.StartDate,
                    EndDate = plan.TargetDate
                };
            }
            else
            {
                CurrentPlanDashboard = new CurrentPlanDashboardModel
                {
                    Name = "Chưa có kế hoạch",
                    Description = "",
                    Progress = 0,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today
                };
            }
            // Nhật ký gần đây
            var statuses = _smokingStatusService.GetSmokingStatusesByUserId(userId)
                .OrderByDescending(s => s.RecordDate)
                .Take(10)
                .ToList();
            RecentJournals = new ObservableCollection<JournalRecord>(
                statuses.Select(s => new JournalRecord
                {
                    Date = s.RecordDate,
                    Cigarettes = s.CigarettesPerDay,
                    Packs = s.PacksPerWeek,
                    CostPerPack = s.CostPerPack,
                    WeeklyCost = s.CostPerPack * s.PacksPerWeek,
                    MonthlyCost = s.CostPerPack * s.PacksPerWeek * 4,
                    TotalCigarettes = s.CigarettesPerDay * 7
                })
            );
        }

        public DashboardViewModel()
        {
            ViewAllNotificationsCommand = new RelayCommand(obj => ViewAllNotifications());
            _notificationService = new NotificationService();

            LoadNotifications();
            LoadQuitStats();
            LoadCurrentPlanAndJournals();
        }

        private void ViewAllNotifications()
        {
            var notificationView = new NotificationView();
            notificationView.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
