using Hangfire.Server;
using WhatTheShop.ApiClient;
using WhatTheShop.DB;

namespace WhatTheShop.Services;

public class AnalyticRawService : BaseService
{
    private readonly WhatTheShopApiClient _worker;

    public AnalyticRawService(WhatTheShopApiClient worker, DbCtx db) : base(db)
    {
        _worker = worker;
    }

    public async Task FetchAnalyticRawPasserbyService(DateTime start, DateTime end, PerformContext context)
    {
        var apiName = "/1/analytic/raw/passerby";
        var dataFetcher = _worker.GetAnalyticRawServicePasserby;
        var dbEntities = Db.AnalyticRawPasserby;

        await RunForMultiResponse(start, end, context, apiName, dbEntities, dataFetcher);
    }
    
    public async Task FetchAnalyticRawServiceVisitor(DateTime start, DateTime end, PerformContext context)
    {
        var apiName = "/1/analytic/raw/visitor";
        var dataFetcher = _worker.GetAnalyticRawServiceVisitor;
        var dbEntities = Db.AnalyticRawServiceVisitor;

        await RunForMultiResponse(start, end, context, apiName, dbEntities, dataFetcher);
    }

    public async Task FetchAnalyticRawServiceVisitorLight(DateTime start, DateTime end, PerformContext context)
    {
        var apiName = "/1/analytic/raw/visitorLight";
        var dataFetcher = _worker.GetAnalyticRawServiceVisitorLight;
        var dbEntities = Db.AnalyticRawServiceVisitorLight;

        await RunForMultiResponse(start, end, context, apiName, dbEntities, dataFetcher);
    }

    public async Task FetchAnalyticRawServiceVisitorMacList(DateTime start, DateTime end, PerformContext context)
    {
        var apiName = "/1/analytic/raw/visitorMacList";
        var dataFetcher = _worker.GetAnalyticRawServiceVisitorMacList;
        var dbEntities = Db.AnalyticRawServiceVisitorMacList;

        await RunForMultiResponse(start, end, context, apiName, dbEntities, dataFetcher);
    }

    public async Task FetchAnalyticRawServicePasserbyMacList(DateTime start, DateTime end, PerformContext context)
    {
        var apiName = "/1/analytic/raw/passerbyMacList";
        var dataFetcher = _worker.GetAnalyticRawServicePasserbyMacList;
        var dbEntities = Db.AnalyticRawServicePasserbyMacList;

        await RunForMultiResponse(start, end, context, apiName, dbEntities, dataFetcher);
    }
}