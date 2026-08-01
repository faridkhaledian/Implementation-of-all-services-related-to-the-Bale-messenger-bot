namespace BaleBotManager.Application.Interfaces
{
    public interface IBaleBotClient
    {
        Task<bool> SendMessageAsync(long chatId, string text, CancellationToken ct = default);

        /// <summary>
        /// Calls Bale's getMe endpoint to verify the stored token is valid.
        /// </summary>
        Task<BaleConnectionResult> TestConnectionAsync(CancellationToken ct = default);
    }

    public class BaleConnectionResult
    {
        public bool Success { get; set; }
        public string? BotName { get; set; }
        public string? Message { get; set; }
    }
}
