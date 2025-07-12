using System.Text;
using System.Text.Json;

namespace Services
{
    public class GeminiAIService
    {
        private readonly string _apiKey;
        private readonly string _modelName;
        private readonly double _temperature;
        private readonly double _topP;
        private readonly int _maxOutputTokens;

        public GeminiAIService(string apiKey, string modelName, double temperature, double topP, int maxOutputTokens)
        {
            _apiKey = apiKey;
            _modelName = modelName;
            _temperature = temperature;
            _topP = topP;
            _maxOutputTokens = maxOutputTokens;
        }

        public async Task<string> GetAIResponseAsync(string userMessage)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_modelName}:generateContent";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = userMessage } } }
                },
                generationConfig = new
                {
                    temperature = _temperature,
                    topP = _topP,
                    maxOutputTokens = _maxOutputTokens
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-goog-api-key", _apiKey);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return $"Lỗi API: {response.StatusCode} - {responseString}";
                }

                using var doc = JsonDocument.Parse(responseString);
                var root = doc.RootElement;

                var text = root
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return text ?? "Không nhận được phản hồi từ AI.";
            }
            catch (Exception ex)
            {
                return "Lỗi Exception: " + ex.Message;
            }
        }
    }
}
