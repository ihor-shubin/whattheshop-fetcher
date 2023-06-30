using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using WhatTheShop.DB;
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
        //await RunAnalyticZones(apiClient, db);
        await RunAnalyticRaw(apiClient, db);
        //await RunAnalyticDevice(apiClient, db);
        //await RunAnalyticSystem(apiClient, db);
        // await RunMediaVisits(apiClient, db);
        //await RunMediaCampaign(apiClient, db);
        //await RunAnalyticSensor(apiClient, db);

        // save excel file
        if (File.Exists(fileName)) File.Delete(fileName);

        workbook.SaveAs(fileName);

        Console.WriteLine("Done...");
        Console.ReadKey();
    }

    private static async Task RunUser(ApiClient apiClient, DbCtx db)
    {
        var service = new UserService(apiClient, db);

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

    private static async Task RunAnalyticRaw(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticRawService(apiClient, db);

        await service.FetchAnalyticRawServicePasserby();
        await service.FetchAnalyticRawServiceVisitor();
        await service.FetchAnalyticRawServiceVisitorLight();
        await service.FetchAnalyticRawServiceVisitorMacList();
        await service.FetchAnalyticRawServicePasserbyMacList();
    }

    private static async Task RunAnalyticDevice(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticDeviceService(apiClient, db);

        await service.FetchAnalyticDeviceCount();
    }

    private static async Task RunAnalyticZones(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticZoneService(apiClient, db);

        await service.FetchAnalyticZonesGeneral();
        await service.FetchAnalyticZonesVenn();
        // await service.FetchAnalyticZonesSankey(); // always empty
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

    private static async Task RunAnalyticSystem(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticSystemService(apiClient, db);

        await service.FetchAnalyticSystemLastUpdate();
        await service.FetchAnalyticSystemQuickLastUpdate();
        await service.FetchAnalyticSystemForceRefresh();
        await service.FetchAnalyticSystemTemporaryTable();
    }

    private static async Task RunMediaVisits(ApiClient apiClient, DbCtx db)
    {
        var service = new MediaVisitService(apiClient, db);

        await service.FetchMediaVisitCount();
        await service.FetchMediaVisitCountDetails();
    }

    private static async Task RunMediaCampaign(ApiClient apiClient, DbCtx dbCtx)
    {
        Console.WriteLine("Fetching /1/media/campaign/list...");
        var campaignList = await apiClient.GetCampaignList(dbCtx.Zones.ToList()); // always empty

        Console.WriteLine("Fetching  /1/media/campaign/count...");
        // requires campaigns so always empty for now
        var campaignCount = await apiClient.GetCampaignCount(dbCtx.Zones.ToList(), campaignList);
    }

    private static async Task RunAnalyticSensor(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticSensorService(apiClient, db);

        await service.FetchAnalyticSensorCount();
        await service.FetchAnalyticSensorCountDetails();

        // it requires sensor or hardware id. It's not available in the API
        // await service.FetchAnalyticSensorCountRaw();  // here
        // await service.FetchAnalyticSensorCountDetailsRaw(); // same here
    }
}