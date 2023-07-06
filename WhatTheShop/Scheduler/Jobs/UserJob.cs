using Hangfire.Console;
using Hangfire.Server;
using WhatTheShop.Services;

namespace WhatTheShop.Scheduler.Jobs;

public class UserJob
{
    private readonly UserService _userService;

    public UserJob(UserService userService)
    {
        _userService = userService;
    }

    public async Task FetchZones(PerformContext context)
    {
        await _userService.FetchZones();
        context.WriteLine("Finished");
    }

    public async Task FetchMonitoring(PerformContext context)
    {
        await _userService.FetchMonitoring();
        context.WriteLine("Finished");
    }

    public async Task FetchZoneInfos(PerformContext context)
    {
        await _userService.FetchZoneInfos();
        context.WriteLine("Finished");
    }

    public async Task FetchDevices(PerformContext context)
    {
        await _userService.FetchDevices();
        context.WriteLine("Finished");
    }
}