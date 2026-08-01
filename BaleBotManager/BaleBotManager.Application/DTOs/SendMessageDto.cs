namespace BaleBotManager.Application.DTOs
{
    public class SendMessageDto
    {
        public long ChatId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
