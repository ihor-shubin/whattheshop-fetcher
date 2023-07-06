using Hangfire.Console;
using Hangfire.Server;
using WhatTheShop.Services;
using WhatTheShop.Utils;

namespace WhatTheShop.Scheduler.Jobs;

public class AnalyticPassingJob
{
    private readonly AnalyticPassingService _service;

    public AnalyticPassingJob(AnalyticPassingService service)
    {
        _service = service;
    }

    public async Task GetAnalyticPassingCount(PerformContext context)
    {
        var dateRange = DateUtils.SplitDateRangeByMonth(2);
        foreach (var dr in dateRange)
        {
            context.WriteLine($"Start processing from {dr.Item1:g} to {dr.Item2:g}");
            await _service.FetchAnalyticPassingCount(dr.Item1, dr.Item2, context);    
        }
        
        context.WriteLine("Finished");
    }
}