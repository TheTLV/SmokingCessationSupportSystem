using BusinessObjects;

namespace DataAccessLayout
{
    public static class NotificationDAO
    {
        public static List<Notification> GetNotificationsByUserId(int userId)
        {
            using var db = new AppDbContext();
            return db.Notifications
                .Where(n => n.UserId == userId && n.IsRead == false)
                .OrderByDescending(n => n.SentDate)
                .ToList();
        }

        public static void AddNotification(Notification notification)
        {
            using var db = new AppDbContext();
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        public static void MarkAsRead(int notificationId)
        {
            using var db = new AppDbContext();
            var notification = db.Notifications.Find(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                db.SaveChanges();
            }
        }

        public static Notification GetNotificationToday(int userId)
        {
            using var db = new AppDbContext();
            var today = DateTime.Today;
            return db.Notifications
                .Where(n => n.UserId == userId && n.SentDate.Date == today)
                .OrderByDescending(n => n.SentDate)
                .FirstOrDefault();
        }

        public static List<Notification> GetAllNotificationsByUserId(int userId)
        {
            using var db = new AppDbContext();
            return db.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.SentDate)
                .ToList();
        }
    }
}
