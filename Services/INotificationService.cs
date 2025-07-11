using BusinessObjects;

namespace Services
{
    public interface INotificationService
    {
        List<Notification> GetNotificationsByUserId(int userId);
        void MarkAsRead(int notificationId);
        void SendDailyNotificationForUser(int userId);
        Notification GetNotificationToday(int userId);
        List<Notification> GetAllNotificationsByUserId(int userId);
    }
}
