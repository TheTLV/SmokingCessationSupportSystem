using BusinessObjects;
using Repositories;

namespace Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository iNotificationRepository;
        private readonly IQuitPlanRepository iQuitPlanRepository;
        private readonly IUserRepository iUserRepository = new UserRepository();

        private static readonly List<string> MotivationalMessages = new List<string>
        {
            "Mỗi ngày không hút thuốc là một chiến thắng nhỏ!",
            "Bạn đang tiến gần hơn tới một cuộc sống khỏe mạnh hơn!",
            "Gia đình và bạn bè luôn tự hào về bạn!",
            "Mỗi phút không hút thuốc là một phút bạn kéo dài tuổi thọ của mình!",
            "Hãy nhớ lý do bạn bắt đầu hành trình này!",
            "Cai thuốc lá là một hành trình, không phải là đích đến!",
            "Bạn không đơn độc trong hành trình này, có rất nhiều người ủng hộ bạn!",
            "Hãy tập trung vào những điều tích cực mà bạn đang đạt được!",
            "Mỗi ngày không hút thuốc là một ngày bạn sống khỏe mạnh hơn!",
            "Hãy nhớ rằng mỗi lần bạn từ chối thuốc lá, bạn đang làm cho cơ thể mình khỏe mạnh hơn!",
        };

        public NotificationService()
        {
            iNotificationRepository = new NotificationRepository();
            iQuitPlanRepository = new QuitPlanRepository();
            iUserRepository = new UserRepository();
        }
        public List<Notification> GetNotificationsByUserId(int userId)
        {
            return iNotificationRepository.GetNotificationsByUserId(userId);
        }

        public void MarkAsRead(int notificationId)
        {
            iNotificationRepository.MarkAsRead(notificationId);
        }

        public string GetMotivationalMessage(QuitPlan? userQuitPlan, int dayCount)
        {
            var random = new Random();
            if (userQuitPlan == null)
            {
                return MotivationalMessages[random.Next(MotivationalMessages.Count)];
            }
            else
            {
                if (dayCount % 2 == 0)
                {
                    return MotivationalMessages[random.Next(MotivationalMessages.Count)];
                }
                else
                {
                    return $"Hãy nhớ lý do bạn muốn cai thuốc: {userQuitPlan.Reason}";
                }
            }
        }

        public void SendDailyNotificationForUser(int userId)
        {
            var quitPlan = iQuitPlanRepository.GetCurrentQuitPlanById(userId);
            string message = null;
            if (quitPlan == null)
            {
                message = GetMotivationalMessage(null, 0);
            }
            else
            {
                int dayCount = (DateTime.Today - quitPlan.StartDate).Days;
                message = GetMotivationalMessage(quitPlan, dayCount);
            }
            var notifications = iNotificationRepository.GetNotificationsByUserId(userId);
            if (!notifications.Any(n => n.SentDate.Date == DateTime.Today))
            {
                iNotificationRepository.AddNotification(new Notification
                {
                    UserId = userId,
                    Message = message,
                    SentDate = DateTime.Now,
                    IsRead = false
                });
            }
        }
    }
}
