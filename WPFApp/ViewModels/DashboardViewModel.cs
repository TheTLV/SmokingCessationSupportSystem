using BusinessObjects;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPFApp.Views;

namespace WPFApp.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
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
            using (var db = new AppDbContext())
            {
                var userId = AppSession.CurrentUser?.Id ?? 0;
                Notifications = new ObservableCollection<Notification>(
                    db.Notifications
                      .Where(n => n.UserId == userId)
                      .OrderByDescending(n => n.SentDate)
                      .Take(10)
                );

                UnreadNotificationCount = Notifications.Count(n => !n.IsRead);
            }
        }

        public DashboardViewModel()
        {
            ViewAllNotificationsCommand = new RelayCommand(obj => ViewAllNotifications());
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
