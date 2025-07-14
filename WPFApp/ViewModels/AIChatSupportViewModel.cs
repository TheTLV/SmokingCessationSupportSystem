using GalaSoft.MvvmLight;
using Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class AIChatSupportViewModel : ViewModelBase
    {
        private readonly Services.GeminiAIService aIService;

        public class ChatMessage
        {
            public string Content { get; set; }
            public bool IsFromUser { get; set; }
            public DateTime Timestamp { get; set; } = DateTime.Now;
        }

        private string _currentMessage;
        public string CurrentMessage
        {
            get => _currentMessage;
            set => Set(ref _currentMessage, value);
        }

        public ObservableCollection<ChatMessage> Messages { get; } = new ObservableCollection<ChatMessage>();

        public ICommand SendCommand { get; }

        public AIChatSupportViewModel()
        {
            var config = WPFApp.App.Configuration.GetSection("GeminiConfig");
            var apiKey = config["ApiKey"];
            var modelName = config["ModelName"];
            var temperature = double.Parse(config.GetSection("GenerationConfig")["Temperature"]);
            var topP = double.Parse(config.GetSection("GenerationConfig")["TopP"]);
            var maxOutputTokens = int.Parse(config.GetSection("GenerationConfig")["MaxOutputTokens"]);

            aIService = new GeminiAIService(apiKey, modelName, temperature, topP, maxOutputTokens);


            SendCommand = new RelayCommand(SendMessage, CanSendMessage);

            Messages.Add(new ChatMessage
            {
                Content = "Xin chào! Tôi là trợ lý AI hỗ trợ cai thuốc lá. Bạn có câu hỏi gì muốn hỏi tôi không?",
                IsFromUser = false
            });
        }

        private bool CanSendMessage()
        {
            return !string.IsNullOrWhiteSpace(CurrentMessage);
        }

        private async void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(CurrentMessage))
                return;

            var userMessage = new ChatMessage
            {
                Content = CurrentMessage,
                IsFromUser = true
            };
            Messages.Add(userMessage);

            CurrentMessage = string.Empty;

            var thinkingMessage = new ChatMessage
            {
                Content = "AI đang suy nghĩ...",
                IsFromUser = false
            };
            Messages.Add(thinkingMessage);

            try
            {
                string aiResponse = await aIService.GetAIResponseAsync(userMessage.Content);
                Messages.Remove(thinkingMessage);
                Messages.Add(new ChatMessage
                {
                    Content = aiResponse,
                    IsFromUser = false
                });
            }
            catch (Exception ex)
            {
                Messages.Remove(thinkingMessage);
                Messages.Add(new ChatMessage
                {
                    Content = "Đã xảy ra lỗi khi kết nối AI: " + ex.Message,
                    IsFromUser = false
                });
            }
        }
    }
}