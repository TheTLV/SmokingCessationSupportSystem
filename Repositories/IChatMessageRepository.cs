using BusinessObjects;

namespace Repositories
{
    public interface IChatMessageRepository
    {
        List<ChatMessage> GetChatMessages(int userId, int coachId);
        void AddChatMessage(ChatMessage message);
        List<int> GetUserChatWithCoah(int coachId);
    }
}
