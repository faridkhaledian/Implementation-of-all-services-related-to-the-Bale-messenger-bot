using BaleBotManager.Application.Interfaces;
using BaleBotManager.Application.Services;
using BaleBotManager.Domain.Interfaces;
using BaleBotManager.Infrastructure.BaleApi;
using BaleBotManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// اتصال دیتابیس
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<IBaleBotClient, BaleBotClient>();
builder.Services.AddScoped<IDashboardMessageService, DashboardMessageService>();
builder.Services.AddScoped<IBotMessageRepository, BotMessageRepository>();
builder.Services.AddScoped<IBotSettingsRepository, BotSettingsRepository>();
builder.Services.AddScoped<ISettingsService, SettingsService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();