using BaleBotManager.Domain.Entities;

namespace BaleBotManager.Application.Interfaces;

public interface IBotUserRepository
{
    Task<BotUser?> GetByChatIdAsync(long chatId);
    Task AddAsync(BotUser user);
    Task UpdateAsync(BotUser user);
    Task<List<BotUser>> GetAllAsync();
}