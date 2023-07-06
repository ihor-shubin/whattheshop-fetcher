using Hangfire;
using Hangfire.Console;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WhatTheShop.ApiClient;
using WhatTheShop.Configuration;
using WhatTheShop.DB;
using WhatTheShop.Scheduler.Hangfire;
using WhatTheShop.Services;

namespace WhatTheShop;

internal class Program
{
    private static WebApplication _webApplication;
    private static AppSettings _appConfiguration;

    private static async Task Main()
    {
        InitConfiguration();
        InitWebApp();

        JobRunner.RegisterUserFetchingJobs();
        JobRunner.RegisterAnalyticPassingFetchingJob();
        JobRunner.RegisterAnalyticRawJobFetchingJob();

        await _webApplication.RunAsync("http://localhost:8888");
    }

    private static void InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json")
            .Build();

        _appConfiguration = config.GetRequiredSection("Settings").Get<AppSettings>()!;
    }

    private static void InitWebApp()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        builder.Services.AddOptions();
        builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.json");
        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Settings"));

        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<AnalyticPassingService>();
        builder.Services.AddScoped<AnalyticRawService>();

        builder.Services.AddHangfire(configuration =>
        {
            configuration.UseConsole();
            configuration.UseInMemoryStorage();
        });

        builder.Services.AddHangfireServer();

        builder.Services.AddDbContext<DbCtx>(opt => opt.UseSqlite(_appConfiguration.ConnectionStrings.Extractor));
        builder.Services.AddSingleton(_ =>
            new WhatTheShopApiClient(
                _appConfiguration.WhatTheShopApi.URL,
                _appConfiguration.WhatTheShopApi.UserName,
                _appConfiguration.WhatTheShopApi.Password));
        
        builder.Logging.SetMinimumLevel(LogLevel.Warning);
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        
        _webApplication = builder.Build();

        _webApplication.UseHangfireDashboard();

        _webApplication.MapGet("/", () => "It works!");
    }
}