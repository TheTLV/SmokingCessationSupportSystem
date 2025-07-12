using BusinessObjects;

namespace DataAccessLayout
{
    public static class ChatMessageDAO
    {
        public static List<ChatMessage> GetChatMessages(int userId, int coachId)
        {
            using var db = new AppDbContext();
            return db.ChatMessages
                .Where(m => (m.SenderId == userId && m.ReceiverId == coachId) ||
                            (m.SenderId == coachId && m.ReceiverId == userId))
                .OrderBy(m => m.SentAt)
                .ToList();
        }

        public static void AddChatMessage(ChatMessage message)
        {
            using var db = new AppDbContext();
            db.ChatMessages.Add(message);
            db.SaveChanges();
        }

        public static List<int> GetUserChatWithCoah(int coachId)
        {
            using var db = new AppDbContext();
            return db.ChatMessages
                .Where(m => m.ReceiverId == coachId)
                .Select(m => m.SenderId)
                .Distinct()
                .ToList();
        }
    }
}
