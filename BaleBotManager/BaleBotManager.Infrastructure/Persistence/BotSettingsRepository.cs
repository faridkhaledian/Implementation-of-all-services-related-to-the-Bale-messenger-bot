using BaleBotManager.Domain.Entities;
using BaleBotManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaleBotManager.Infrastructure.Persistence
{
    public class BotSettingsRepository : IBotSettingsRepository
    {
        private readonly AppDbContext _context;

        public BotSettingsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BotSettings?> GetAsync()
        {
            // There is only ever one settings row for now.
            return await _context.BotSettings.FirstOrDefaultAsync();
        }

        public async Task SaveAsync(BotSettings settings)
        {
            if (settings.Id == 0)
            {
                _context.BotSettings.Add(settings);
            }
            else
            {
                _context.BotSettings.Update(settings);
            }

            await _context.SaveChangesAsync();
        }
    }
}
