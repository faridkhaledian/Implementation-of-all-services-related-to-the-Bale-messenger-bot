using BaleBotManager.Application.Interfaces;
using BaleBotManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaleBotManager.Infrastructure.Persistence;

public class BotUserRepository : IBotUserRepository
{
    private readonly AppDbContext _context;

    public BotUserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BotUser?> GetByChatIdAsync(long chatId) =>
        await _context.BotUsers.FirstOrDefaultAsync(u => u.ChatId == chatId);

    public async Task AddAsync(BotUser user)
    {
        _context.BotUsers.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BotUser user)
    {
        _context.BotUsers.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<BotUser>> GetAllAsync() =>
        await _context.BotUsers.ToListAsync();
}