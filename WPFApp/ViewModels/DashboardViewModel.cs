using BusinessObjects;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPFApp.Views;

namespace WPFApp.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly INotificationService _notificationService;

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
            }
        }

        public ICommand ViewAllNotificationsCommand { get; }

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

        public DashboardViewModel()
        {
            ViewAllNotificationsCommand = new RelayCommand(obj => ViewAllNotifications());
            _notificationService = new NotificationService();

            LoadNotifications();
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
