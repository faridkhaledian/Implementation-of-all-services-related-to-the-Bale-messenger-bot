namespace BaleBotManager.Domain.Entities
{
    public class BotMessage
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public string Text { get; set; }
        public bool IsFromBot { get; set; }
        public DateTime SentAt { get; set; }
    }
}
