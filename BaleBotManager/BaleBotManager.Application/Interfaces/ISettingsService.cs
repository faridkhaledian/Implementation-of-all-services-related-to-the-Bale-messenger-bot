using BaleBotManager.Application.DTOs;

namespace BaleBotManager.Application.Interfaces
{
    public interface ISettingsService
    {
        Task<string?> GetTokenAsync();
        Task UpdateTokenAsync(UpdateTokenDto dto);
    }
}
