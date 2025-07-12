using BusinessObjects;

namespace Services
{
    public interface IChatMessageService
    {
        List<ChatMessage> GetChatMessages(int userId, int coachId);
        void AddMessage(ChatMessage message);
        List<int> GetUserChatWithCoah(int coachId);
    }
}
