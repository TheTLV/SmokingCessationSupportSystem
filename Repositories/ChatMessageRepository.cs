using BusinessObjects;
using DataAccessLayout;

namespace Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        public void AddChatMessage(ChatMessage message)
        {
            ChatMessageDAO.AddChatMessage(message);
        }

        public List<ChatMessage> GetChatMessages(int userId, int coachId)
        {
            return ChatMessageDAO.GetChatMessages(userId, coachId);
        }

        public List<int> GetUserChatWithCoah(int coachId)
        {
            return ChatMessageDAO.GetUserChatWithCoah(coachId);
        }
    }
}
