using BusinessObjects;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFApp.ViewModels
{
    public class CoachChatViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly IChatMessageService chatMessageService;

        private readonly int _currentCoachId;
        private User _selectedPatient;
        private string _newMessage;

        public ObservableCollection<User> Patients { get; } = new ObservableCollection<User>();
        public ObservableCollection<ChatMessageViewModel> Messages { get; } = new ObservableCollection<ChatMessageViewModel>();

        public User SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                if (SetProperty(ref _selectedPatient, value))
                {
                    LoadMessages();
                    MarkMessagesAsRead();
                    OnPropertyChanged(nameof(IsPatientSelected));
                }
            }
        }

        public bool IsPatientSelected => SelectedPatient != null;

        public string NewMessage
        {
            get => _newMessage;
            set
            {
                SetProperty(ref _newMessage, value);
                SendCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand SendCommand { get; }
        public RelayCommand LoadPatientsCommand { get; }
        public RelayCommand SelectPatientCommand { get; }

        public CoachChatViewModel(int currentCoachId)
        {
            _currentCoachId = currentCoachId;
            _userService = new UserService();
            chatMessageService = new ChatMessageService();

            // Initialize commands
            SendCommand = new RelayCommand(SendMessage, CanSendMessage);
            LoadPatientsCommand = new RelayCommand(LoadPatients);
            SelectPatientCommand = new RelayCommand(param => SelectedPatient = param as User);

            // Load sample data
            LoadPatients();
        }

        private void LoadPatients(object parameter = null)
        {
            List<int> listId = chatMessageService.GetUserChatWithCoah(_currentCoachId);
            var patients = _userService.GetUserChatWithCoach(listId);
            Patients.Clear();
            foreach (var patient in patients)
            {
                Patients.Add(patient);
            }
        }

        private void LoadMessages()
        {
            Messages.Clear();

            if (SelectedPatient == null) return;

            var Meesages = chatMessageService.GetChatMessages(SelectedPatient.Id, _currentCoachId);
            foreach (var message in Meesages.OrderBy(m => m.SentAt))
            {
                Messages.Add(new ChatMessageViewModel(message)
                {
                    IsIncomingMessage = message.SenderId != _currentCoachId
                });
            }
        }

        private void MarkMessagesAsRead()
        {
            if (SelectedPatient == null) return;

            foreach (var message in Messages.Where(m => !m.IsRead && m.IsIncomingMessage))
            {
                message.IsRead = true;
            }
        }

        private void SendMessage(object parameter)
        {
            if (!CanSendMessage(parameter)) return;

            var newMsg = new ChatMessage
            {
                SenderId = _currentCoachId,
                ReceiverId = SelectedPatient.Id,
                Message = NewMessage,
                SentAt = DateTime.Now,
                IsRead = false,
            };

            Messages.Add(new ChatMessageViewModel(newMsg)
            {
                IsIncomingMessage = false
            });

            chatMessageService.AddMessage(newMsg);

            NewMessage = string.Empty;
        }

        private bool CanSendMessage(object parameter) =>
            !string.IsNullOrWhiteSpace(NewMessage) && SelectedPatient != null;
    }

    public class ChatMessageViewModel : BaseViewModel
    {
        private readonly ChatMessage _model;

        public ChatMessageViewModel(ChatMessage model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public int Id => _model.Id;
        public int SenderId => _model.SenderId;
        public int ReceiverId => _model.ReceiverId;
        public string Content => _model.Message;
        public DateTime SentAt => _model.SentAt;
        public bool IsRead
        {
            get => _model.IsRead;
            set
            {
                if (_model.IsRead != value)
                {
                    _model.IsRead = value;
                    OnPropertyChanged();
                }
            }
        }

        // For UI binding
        public bool IsIncomingMessage { get; set; }
        public string TimeDisplay => SentAt.ToString("HH:mm");
    }

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}