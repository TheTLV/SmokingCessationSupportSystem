using BusinessObjects;
using Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class NotificationViewModel
    {
        public ObservableCollection<Notification> Notifications { get; set; }
        public ICommand MarkAsReadCommand { get; }
        public ICommand MarkAllAsReadCommand { get; }
        public ICommand CloseCommand { get; }

        private readonly INotificationService _notificationService;

        public NotificationViewModel()
        {
            _notificationService = new NotificationService();

            Notifications = new ObservableCollection<Notification>();
            LoadNotifications(AppSession.CurrentUser.Id);
            // Khởi tạo các command
            MarkAsReadCommand = new RelayCommand(obj => MarkAsRead((int)obj));
            MarkAllAsReadCommand = new RelayCommand(obj => MarkAllAsRead());
            CloseCommand = new RelayCommand(obj => CloseWindow());
        }

        public void LoadNotifications(int userId)
        {
            var notifications = _notificationService.GetNotificationsByUserId(userId);
            Notifications.Clear();
            foreach (var notification in notifications)
            {
                Notifications.Add(notification);
            }
        }

        private void MarkAsRead(int notificationId)
        {
            _notificationService.MarkAsRead(notificationId);
            LoadNotifications(AppSession.CurrentUser.Id);
            MessageBox.Show("Thông báo đã được đánh dấu là đã đọc.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MarkAllAsRead()
        {
            // Logic đánh dấu tất cả đã đọc
            foreach (var notification in Notifications)
            {
                if (!notification.IsRead)
                {
                    _notificationService.MarkAsRead(notification.Id);
                    notification.IsRead = true; // Cập nhật trạng thái trong ObservableCollection
                }
            }
            MessageBox.Show("Tất cả thông báo đã được đánh dấu là đã đọc.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadNotifications(AppSession.CurrentUser.Id);
        }

        private void CloseWindow()
        {
        }
    }
}