using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using WhatTheShop.DB;
using WhatTheShop.Models;
using WhatTheShop.Services;

namespace WhatTheShop;

internal class Program
{
    internal const bool OverwriteDb = false;

    private static async Task Main()
    {
        await using var db = new DbCtx();
        await db.Database.EnsureCreatedAsync();

        db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        var fileName = "whattheshop.xlsx";
        var workbook = new XLWorkbook();
        var apiClient = new ApiClient();

        await RunUser(apiClient, db);

        // await RunAnalyticPasserBy(apiClient, db);
        // await RunAnalyticPassing(apiClient, db);
        //await RunAnalyticVisitor(apiClient, db);
        //await RunAnalyticsVisit(apiClient, wtsToken, zones, devices);
        //await RunAnalyticsRecord(apiClient, wtsToken, zones, devices);
        //await RunAnalyticsInstore(apiClient, wtsToken, zones, devices);
        await RunAnalyticZones(apiClient, db);
        //await RunAnalyticsRaw(apiClient, wtsToken, zones, devices);
        //await RunAnalyticDevice(apiClient, db);
        //await RunAnalyticsSystem(apiClient, wtsToken, zones, devices);
        //await RunMediaVisits(apiClient, wtsToken, zones, devices);
        //await RunMediaCampaign(apiClient, wtsToken, zones, devices);
        //await RunAnalyticSensor(apiClient, wtsToken, zones, devices);

        // save excel file
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        workbook.SaveAs(fileName);

        Console.WriteLine("Done...");
        Console.ReadKey();
    }

    private static async Task RunUser(ApiClient apiClient, DbCtx db)
    {
        var service = new UserService(apiClient, db, OverwriteDb);

        await service.FetchZones();
        await service.FetchZoneInfos();
        await service.FetchDevices();
        await service.FetchMonitoring();
    }

    private static async Task RunAnalyticPasserBy(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticPasserbyService(apiClient, db);

        await service.FetchAnalyticPasserbyCount();
        await service.FetchAnalyticPasserbyCountDetails();
        await service.FetchAnalyticPasserbyCountHour();
        await service.FetchAnalyticPasserbyCountDay();
        // await  service.AnalyticPasserbyCountSum(); /* invalid api return */
        await service.FetchAnalyticPasserbyBestTimes();
        // await  service.AnalyticPasserbyCountCommons(); /* invalid api return */
    }

    private static async Task RunAnalyticPassing(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticPassingService(apiClient, db);

        await service.FetchAnalyticPassingCount();
        await service.FetchAnalyticPassingCountDetails();
        await service.FetchAnalyticPassingCountHourDetails();
    }

    private static async Task RunAnalyticVisitor(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticVisitorService(apiClient, db);

        await service.FetchAnalyticVisitorCount();
        await service.FetchAnalyticVisitorCountDetails();
        await service.FetchAnalyticVisitorDuration();
        await service.FetchAnalyticVisitorDurationDetails();
        await service.FetchAnalyticVisitorBestTimes();
        await service.FetchAnalyticVisitorCountHour();
        await service.FetchAnalyticVisitorCountHourDay(); // !
        await service.FetchAnalyticVisitorCountHourDayDetails();
        await service.FetchAnalyticVisitorCountHourDayStart();
        await service.FetchAnalyticVisitorCountSum();
        await service.FetchAnalyticVisitorCountCommon();
    }

     private static async Task RunAnalyticSensor(ApiClient apiClient, List<Zone> zones, List<Device> devices)
    {
        Console.WriteLine("Fetching /1/analytic/sensor/count...");
        var analyticSensorCount = await apiClient.GetAnalyticSensorCount(zones);
    }

    private static async Task RunMediaCampaign(ApiClient apiClient, List<Zone> zones)
    {
        Console.WriteLine("Fetching /1/media/campaign/list...");
        var campaignList = await apiClient.GetCampaignList(zones);

        Console.WriteLine("Fetching  /1/media/campaign/count...");
        var campaignCount = await apiClient.GetCampaignCount(zones, campaignList);
    }

    private static async Task RunMediaVisits(ApiClient apiClient)
    {

    }

    private static async Task RunAnalyticsSystem(ApiClient apiClient)
    {

    }

    private static async Task RunAnalyticDevice(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticDeviceService(apiClient, db);

        await service.FetchAnalyticDeviceCount();
    }

    private static async Task RunAnalyticsRaw(ApiClient apiClient)
    {

    }

    private static async Task RunAnalyticZones(ApiClient apiClient)
    {

    }

    private static async Task RunAnalyticsInstore(ApiClient apiClient)
    {
    }

    private static async Task RunAnalyticsRecord(ApiClient apiClient)
    {
    }

    private static async Task RunAnalyticsVisit(ApiClient apiClient)
    {
    }
}