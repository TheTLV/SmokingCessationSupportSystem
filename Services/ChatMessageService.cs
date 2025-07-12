using BusinessObjects;
using Repositories;

namespace Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatMessageService()
        {
            _chatMessageRepository = new ChatMessageRepository();
        }

        public List<ChatMessage> GetChatMessages(int userId, int coachId)
        {
            return _chatMessageRepository.GetChatMessages(userId, coachId);
        }

        public void AddMessage(ChatMessage message)
        {
            _chatMessageRepository.AddChatMessage(message);
        }

        public List<int> GetUserChatWithCoah(int coachId)
        {
            return _chatMessageRepository.GetUserChatWithCoah(coachId);
        }
    }
}
