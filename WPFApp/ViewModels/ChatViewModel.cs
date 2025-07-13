using BusinessObjects;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        private readonly IChatMessageService chatMessageService;

        private readonly int _currentUserId;
        private readonly Coach _currentCoach;

        public ObservableCollection<ChatMessage> Messages { get; } = new ObservableCollection<ChatMessage>();

        private string _newMessage;
        public string NewMessage
        {
            get => _newMessage;
            set
            {
                _newMessage = value;
                OnPropertyChanged();
            }
        }

        public int CurrentUserId => _currentUserId;

        public ICommand SendCommand { get; }

        public ChatViewModel(int currentUserId, Coach coach)
        {
            _currentUserId = currentUserId;
            _currentCoach = coach;

            chatMessageService = new ChatMessageService();

            SendCommand = new RelayCommand(SendMessage, CanSendMessage);

            LoadMessages();
        }

        private void LoadMessages()
        {
            var chatMessage = chatMessageService.GetChatMessages(_currentUserId, _currentCoach.UserId);

            foreach (var message in chatMessage.OrderBy(m => m.SentAt))
            {
                Messages.Add(message);
            }
        }

        private void SendMessage(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(NewMessage))
            {
                var newMsg = new ChatMessage
                {
                    SenderId = _currentUserId,
                    ReceiverId = _currentCoach.UserId,
                    Message = NewMessage,
                    SentAt = DateTime.Now,
                    IsRead = false
                };

                chatMessageService.AddMessage(newMsg);
                Messages.Add(newMsg);
                NewMessage = string.Empty;
            }
        }

        private bool CanSendMessage(object parameter) => !string.IsNullOrWhiteSpace(NewMessage);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}