using BaleBotManager.Application.DTOs;
using BaleBotManager.Application.Interfaces;
using BaleBotManager.Domain.Entities;
using BaleBotManager.Domain.Interfaces;

namespace BaleBotManager.Application.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IBotSettingsRepository _settingsRepository;

        public SettingsService(IBotSettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<string?> GetTokenAsync()
        {
            var settings = await _settingsRepository.GetAsync();
            return settings?.Token;
        }

        public async Task UpdateTokenAsync(UpdateTokenDto dto)
        {
            var settings = await _settingsRepository.GetAsync() ?? new BotSettings();
            settings.Token = dto.Token.Trim();
            settings.UpdatedAt = DateTime.UtcNow;

            await _settingsRepository.SaveAsync(settings);
        }
    }
}
