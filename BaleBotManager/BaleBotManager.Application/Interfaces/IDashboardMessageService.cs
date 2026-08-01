using BaleBotManager.Application.DTOs;

namespace BaleBotManager.Application.Interfaces
{
    public interface IDashboardMessageService
    {
        Task<bool> SendTextMessageAsync(SendMessageDto dto);
    }
}
