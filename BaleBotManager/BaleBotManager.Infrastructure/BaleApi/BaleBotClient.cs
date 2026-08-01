using System.Net.Http.Json;
using System.Text.Json;
using BaleBotManager.Application.Interfaces;

namespace BaleBotManager.Infrastructure.BaleApi
{
    public class BaleBotClient : IBaleBotClient
    {
        private readonly HttpClient _httpClient;
        private readonly ISettingsService _settingsService;

        public BaleBotClient(HttpClient httpClient, ISettingsService settingsService)
        {
            _httpClient = httpClient;
            _settingsService = settingsService;
        }

        public async Task<bool> SendMessageAsync(long chatId, string text, CancellationToken ct = default)
        {
            var token = await _settingsService.GetTokenAsync();
            if (string.IsNullOrWhiteSpace(token))
                return false;

            var url = $"https://tapi.bale.ai/bot{token}/sendMessage";
            var payload = new { chat_id = chatId, text };

            var response = await _httpClient.PostAsJsonAsync(url, payload, ct);
            return response.IsSuccessStatusCode;
        }

        public async Task<BaleConnectionResult> TestConnectionAsync(CancellationToken ct = default)
        {
            var token = await _settingsService.GetTokenAsync();
            if (string.IsNullOrWhiteSpace(token))
            {
                return new BaleConnectionResult { Success = false, Message = "توکنی ثبت نشده است." };
            }

            try
            {
                var url = $"https://tapi.bale.ai/bot{token}/getMe";
                var response = await _httpClient.GetAsync(url, ct);

                if (!response.IsSuccessStatusCode)
                {
                    return new BaleConnectionResult { Success = false, Message = "توکن نامعتبر است." };
                }

                using var stream = await response.Content.ReadAsStreamAsync(ct);
                using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: ct);

                var root = doc.RootElement;
                var botName = root.TryGetProperty("result", out var result) &&
                              result.TryGetProperty("first_name", out var name)
                    ? name.GetString()
                    : "بات بله";

                return new BaleConnectionResult { Success = true, BotName = botName };
            }
            catch (Exception ex)
            {
                return new BaleConnectionResult { Success = false, Message = "خطا در برقراری ارتباط: " + ex.Message };
            }
        }
    }
}
