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
        var excelFullFilename = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            $"WhatTheShop_dump_{DateTime.Now:dd-MMMM-yyyy_hh-mm-ss_(zz)}.xlsx"
        );

        var workbook = new XLWorkbook();
        var apiClient = new ApiClient();

        /* fetch data from API to local db */
        async Task FetchData(DbCtx database, ApiClient client)
        {
            await RunUser(client, database);
            await RunAnalyticPasserBy(client, database);
            await RunAnalyticPassing(client, database);
            await RunAnalyticVisitor(client, database);
            await RunAnalyticVisit(client, database);
            await RunAnalyticRecord(client, database);
            await RunAnalyticInstore(client, database);
            await RunAnalyticZones(client, database);
            await RunAnalyticRaw(client, database);
            await RunAnalyticDevice(client, database);
            await RunAnalyticSystem(client, database);
            await RunMediaVisits(client, database);
            await RunMediaCampaign(client, database);
            await RunAnalyticSensor(client, database);
        }

        // await FetchData(db, apiClient);

        await RunUser(apiClient, db);
        var tasks = new []
        {
            (DbCtx _db) =>
            {
                return RunAnalyticPasserBy(apiClient, _db);
            },
            (DbCtx _db) =>
            {
                
                return RunAnalyticPassing(apiClient, _db);
            },
            (DbCtx _db) =>
            {
                
                return RunAnalyticVisitor(apiClient, _db);
            },
            (DbCtx _db) =>
            {
                
                return RunAnalyticVisit(apiClient, _db);
            },
            (DbCtx _db) =>
            {
                
                return RunAnalyticRecord(apiClient, _db);
            },
            (DbCtx _db) =>
            {
                
                return RunAnalyticInstore(apiClient, _db);
            },
            (DbCtx _db) =>
            {
                
                return RunAnalyticZones(apiClient, _db);
            },
            (DbCtx _db) =>
            {
                
                return RunAnalyticRaw(apiClient, _db);
            },
            (DbCtx _db) =>
            {
                
                return RunAnalyticDevice(apiClient, _db);
            },
            (DbCtx _db) =>
            {
                
                return RunAnalyticSystem(apiClient, _db);
            },
            (DbCtx _db) =>
            {
                
                return RunMediaVisits(apiClient, _db);
            },
            (DbCtx _db) =>
            {
                
                return RunMediaCampaign(apiClient, _db);
            },
            (DbCtx _db) => // 1
            {
                
                return RunAnalyticSensor(apiClient, _db);
            }
        }.Select(x => x).ToArray();

        Parallel.ForEach(
            tasks,
            new ParallelOptions { MaxDegreeOfParallelism = 13 },
            x =>
        {
            using var _db = new DbCtx();
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            x(_db).Wait();
        });

        // save excel file
        workbook.SaveAs(excelFullFilename);

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

    private static async Task RunAnalyticVisit(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticVisitService(apiClient, db);

        await service.FetchAnalyticVisitCount();
        await service.FetchAnalyticVisitCountDetails();
        await service.FetchAnalyticVisitCountHourDetails();
        await service.FetchAnalyticVisitDuration();
        await service.FetchAnalyticVisitDurationDetails();
        await service.FetchAnalyticVisitCountHour(); // always empty for some reasons...
        await service.FetchAnalyticVisitCountHourDay();  // always empty for some reasons...
        await service.FetchAnalyticVisitCountHourDayStart();  // always empty for some reasons...
    }

    private static async Task RunAnalyticRecord(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticRecordService(apiClient, db);

        await service.FetchAnalyticRecordVisitor();
        await service.FetchAnalyticRecordVisit();
        await service.FetchAnalyticRecordDay();
        await service.FetchAnalyticRecordVisitDay();
        await service.FetchAnalyticRecordTime();
        await service.FetchAnalyticRecordPasserby();
        await service.FetchAnalyticRecordPasserbyDay();
        await service.FetchAnalyticRecordPasserbyTime();
    }

    private static async Task RunAnalyticInstore(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticInstoreService(apiClient, db);

        await service.FetchAnalyticInstoreCount();
        await service.FetchAnalyticInstoreCountNew();
        await service.FetchAnalyticInstoreCountDetails();
        await service.FetchAnalyticInstoreDuration();
        await service.FetchAnalyticInstorePageView();
        await service.FetchAnalyticInstoreSubscribe();
        await service.FetchAnalyticInstoreUsers();
        await service.FetchAnalyticInstoreOpinions();
        await service.FetchAnalyticInstoreGain();
    }

    private static async Task RunAnalyticZones(ApiClient apiClient, DbCtx db)
    {
        var service = new AnalyticZoneService(apiClient, db);

        await service.FetchAnalyticZonesGeneral();
        await service.FetchAnalyticZonesVenn();
        //await service.FetchAnalyticZonesSankey(); // always empty, need work
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

    private static async Task RunMediaCampaign(ApiClient apiClient, DbCtx db)
    {
        var service = new MediaCampaignService(apiClient, db);

        await service.FetchMediaCampaignList(); // always empty
        await service.FetchMediaCampaignCount(); // missing data from FetchMediaCampaignList()
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