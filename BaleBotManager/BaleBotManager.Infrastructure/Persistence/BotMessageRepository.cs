using BaleBotManager.Domain.Entities;
using BaleBotManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaleBotManager.Infrastructure.Persistence
{
    public class BotMessageRepository : IBotMessageRepository
    {
        private readonly AppDbContext _context;

        public BotMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BotMessage message)
        {
            _context.BotMessages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}