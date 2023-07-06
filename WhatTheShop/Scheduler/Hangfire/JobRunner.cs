using Hangfire;
using WhatTheShop.Scheduler.Jobs;

namespace WhatTheShop.Scheduler.Hangfire;

public class JobRunner
{
    public static void RegisterUserFetchingJobs()
    {
        BackgroundJob.Enqueue<UserJob>(x => x.FetchZones(null!));
        BackgroundJob.Enqueue<UserJob>(x => x.FetchDevices(null!));
        BackgroundJob.Enqueue<UserJob>(x => x.FetchMonitoring(null!));
        BackgroundJob.Enqueue<UserJob>(x => x.FetchZoneInfos(null!));
    }

    public static void RegisterAnalyticPassingFetchingJob()
    {
        BackgroundJob.Enqueue<AnalyticPassingJob>(x => x.GetAnalyticPassingCount(null!));
    }

    public static void RegisterAnalyticRawJobFetchingJob()
    {
        int monthsToFetch = 24;
        BackgroundJob.Enqueue<AnalyticRawJob>(x => x.GetAnalyticPassingCount(null!, monthsToFetch));
    }
}