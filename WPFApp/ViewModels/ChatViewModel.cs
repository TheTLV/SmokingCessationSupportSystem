using BusinessObjects;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        private readonly int _currentUserId; // ID của người dùng hiện tại
        private readonly CoachViewModel _currentCoach;

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

        public ChatViewModel(int currentUserId, CoachViewModel coach)
        {
            _currentUserId = currentUserId;
            _currentCoach = coach;

            SendCommand = new RelayCommand(SendMessage, CanSendMessage);

            LoadMessages();
        }

        private void LoadMessages()
        {
            // TODO: Thay thế bằng code lấy tin nhắn từ database hoặc API
            var sampleMessages = new List<ChatMessage>
            {
                new ChatMessage
                {
                    Id = 1,
                    SenderId = _currentCoach.Id,
                    ReceiverId = _currentUserId,
                    Message = "Xin chào! Tôi có thể giúp gì cho bạn?",
                    SentAt = DateTime.Now.AddMinutes(-30),
                    IsRead = true
                },
                new ChatMessage
                {
                    Id = 2,
                    SenderId = _currentUserId,
                    ReceiverId = _currentCoach.Id,
                    Message = "Tôi cần tư vấn về chế độ tập luyện",
                    SentAt = DateTime.Now.AddMinutes(-15),
                    IsRead = true
                }
            };

            foreach (var message in sampleMessages.OrderBy(m => m.SentAt))
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
                    ReceiverId = _currentCoach.Id,
                    Message = NewMessage,
                    SentAt = DateTime.Now,
                    IsRead = false
                };

                // TODO: Thêm logic gửi tin nhắn đến database/API ở đây

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