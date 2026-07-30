using BaleBotManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaleBotManager.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<BotUser> BotUsers { get; set; }
    public DbSet<BotMessage> BotMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BotUser>()
            .HasIndex(u => u.ChatId)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}