using BusinessObjects;

namespace Repositories
{
    public interface INotificationRepository
    {
        List<Notification> GetNotificationsByUserId(int userId);
        void AddNotification(Notification notification);
        void MarkAsRead(int notificationId);
    }
}
