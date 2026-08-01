using BaleBotManager.Domain.Entities;

namespace BaleBotManager.Domain.Interfaces
{
    public interface IBotSettingsRepository
    {
        Task<BotSettings?> GetAsync();
        Task SaveAsync(BotSettings settings);
    }
}
