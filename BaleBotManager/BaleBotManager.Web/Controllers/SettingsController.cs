using BaleBotManager.Application.DTOs;
using BaleBotManager.Application.Interfaces;
using BaleBotManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaleBotManager.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;
        private readonly IBaleBotClient _baleBotClient;

        public SettingsController(ISettingsService settingsService, IBaleBotClient baleBotClient)
        {
            _settingsService = settingsService;
            _baleBotClient = baleBotClient;
        }

        public async Task<IActionResult> Index()
        {
            var token = await _settingsService.GetTokenAsync();
            return View(new SettingsViewModel { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> SaveToken(SettingsViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Token))
            {
                model.Success = false;
                return View("Index", model);
            }

            await _settingsService.UpdateTokenAsync(new UpdateTokenDto { Token = model.Token });
            model.Success = true;
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> TestConnection()
        {
            var result = await _baleBotClient.TestConnectionAsync();
            return Json(new
            {
                success = result.Success,
                botName = result.BotName,
                message = result.Message
            });
        }
    }
}
