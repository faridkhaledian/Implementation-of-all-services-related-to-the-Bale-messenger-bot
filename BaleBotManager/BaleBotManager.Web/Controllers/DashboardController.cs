using BaleBotManager.Application.DTOs;
using BaleBotManager.Application.Interfaces;
using BaleBotManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaleBotManager.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardMessageService _dashboardMessageService;

        public DashboardController(IDashboardMessageService dashboardMessageService)
        {
            _dashboardMessageService = dashboardMessageService;
        }

        public IActionResult Index()
        {
            return View(new SendMessageViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            var dto = new SendMessageDto { ChatId = model.ChatId, Text = model.Text };
            var result = await _dashboardMessageService.SendTextMessageAsync(dto);

            model.Success = result;
            return View("Index", model);
        }
    }
}