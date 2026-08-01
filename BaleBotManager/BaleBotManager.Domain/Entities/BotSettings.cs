namespace BaleBotManager.Domain.Entities
{
    public class BotSettings
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
    }
}
