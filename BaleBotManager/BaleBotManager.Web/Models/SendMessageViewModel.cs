namespace BaleBotManager.Web.Models
{
    public class SendMessageViewModel
    {
        public long ChatId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool? Success { get; set; }
    }
}