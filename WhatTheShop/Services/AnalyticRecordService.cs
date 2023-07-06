using WhatTheShop.ApiClient;
using WhatTheShop.DB;
using WhatTheShop.Models;

namespace WhatTheShop.Services;

public class AnalyticRecordService
{
    private readonly WhatTheShopApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public AnalyticRecordService(WhatTheShopApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchAnalyticRecordVisitor()
    {
    }

    public async Task FetchAnalyticRecordVisit()
    {
    }

    public async Task FetchAnalyticRecordDay()
    {
    }

    public async Task FetchAnalyticRecordVisitDay()
    {
    }

    public async Task FetchAnalyticRecordTime()
    {
    }

    public async Task FetchAnalyticRecordPasserby()
    {
    }

    public async Task FetchAnalyticRecordPasserbyDay()
    {
    }

    public async Task FetchAnalyticRecordPasserbyTime()
    {
    }
}