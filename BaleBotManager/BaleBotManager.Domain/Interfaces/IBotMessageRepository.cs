using BaleBotManager.Domain.Entities;

namespace BaleBotManager.Domain.Interfaces
{
    public interface IBotMessageRepository
    {
        Task AddAsync(BotMessage message);
    }
}
