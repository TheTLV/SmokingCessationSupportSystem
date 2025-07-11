using BusinessObjects;
using DataAccessLayout;

namespace Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public List<Notification> GetNotificationsByUserId(int userId)
        {
            return NotificationDAO.GetNotificationsByUserId(userId);
        }

        public void AddNotification(Notification notification)
        {
            NotificationDAO.AddNotification(notification);
        }

        public void MarkAsRead(int notificationId)
        {
            NotificationDAO.MarkAsRead(notificationId);
        }
    }
}
