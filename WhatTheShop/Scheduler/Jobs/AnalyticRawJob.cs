using Hangfire.Console;
using Hangfire.Server;
using WhatTheShop.Models;
using WhatTheShop.Services;
using WhatTheShop.Utils;

namespace WhatTheShop.Scheduler.Jobs;

public class AnalyticRawJob
{
    private readonly AnalyticRawService _service;

    public AnalyticRawJob(AnalyticRawService service)
    {
        _service = service;
    }

    public async Task GetAnalyticPassingCount(PerformContext context, int monthCount)
    {
        var dateRange = DateUtils.SplitDateRangeByMonth(monthCount);
        int i = 0;
        foreach (var dr in dateRange)
        {
            context.WriteLine($"Start processing from {dr.Item1:g} to {dr.Item2:g}");
            context.WriteProgressBar("TimeZonesProgress", (i + 1) / dateRange.Count() * 100);
            await _service.FetchAnalyticRawPasserbyService(dr.Item1, dr.Item2, context);    
        }
        
        context.WriteLine("Finished");
    }
}