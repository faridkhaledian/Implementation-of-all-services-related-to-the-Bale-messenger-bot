using BaleBotManager.Application.DTOs;
using BaleBotManager.Application.Interfaces;
using BaleBotManager.Domain.Entities;
using BaleBotManager.Domain.Interfaces;

namespace BaleBotManager.Application.Services
{
    public class DashboardMessageService : IDashboardMessageService
    {
        private readonly IBaleBotClient _baleBotClient;
        private readonly IBotMessageRepository _messageRepository;

        public DashboardMessageService(
            IBaleBotClient baleBotClient,
            IBotMessageRepository messageRepository)
        {
            _baleBotClient = baleBotClient;
            _messageRepository = messageRepository;
        }

        public async Task<bool> SendTextMessageAsync(SendMessageDto dto)
        {
            var success = await _baleBotClient.SendMessageAsync(dto.ChatId, dto.Text);

            var message = new BotMessage
            {
                ChatId = dto.ChatId,
                Text = dto.Text,
                IsFromBot = true,
                SentAt = DateTime.UtcNow
            };
            await _messageRepository.AddAsync(message);

            return success;
        }
    }
}