using WhatTheShop.DB;
using WhatTheShop.Models;
using WhatTheShop.Utils;

namespace WhatTheShop.Services;

public class AnalyticInstoreService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public AnalyticInstoreService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchAnalyticInstoreCount()
    {
        var apiName = "/1/analytic/instore/count";
        Console.WriteLine("Fetching {0}...", apiName);

        _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
        _db.AAStatus.Add(new Status(apiName, 100));

        await _db.SaveChangesAsync();
    }

    public async Task FetchAnalyticInstoreCountNew()
    {
        var apiName = "/1/analytic/instore/countnew";
        Console.WriteLine("Fetching {0}...", apiName);

        _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
        _db.AAStatus.Add(new Status(apiName, 100));

        await _db.SaveChangesAsync();
    }

    public async Task FetchAnalyticInstoreCountDetails()
    {
        var apiName = "/1/analytic/instore/countdetails";
        Console.WriteLine("Fetching {0}...", apiName);

        _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
        _db.AAStatus.Add(new Status(apiName, 100));

        await _db.SaveChangesAsync();
    }

    public async Task FetchAnalyticInstoreDuration()
    {
        var apiName = "/1/analytic/instore/duration";
        Console.WriteLine("Fetching {0}...", apiName);

        _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
        _db.AAStatus.Add(new Status(apiName, 100));

        await _db.SaveChangesAsync();
    }

    public async Task FetchAnalyticInstorePageView()
    {
        var apiName = "/1/analytic/instore/pageview";
        Console.WriteLine("Fetching {0}...", apiName);

        _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
        _db.AAStatus.Add(new Status(apiName, 100));

        await _db.SaveChangesAsync();
    }

    public async Task FetchAnalyticInstoreSubscribe()
    {
        var apiName = "/1/analytic/instore/subscribe";
        Console.WriteLine("Fetching {0}...", apiName);

        _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
        _db.AAStatus.Add(new Status(apiName, 100));

        await _db.SaveChangesAsync();
    }

    public async Task FetchAnalyticInstoreUsers()
    {
        var apiName = "/1/analytic/instore/users";
        Console.WriteLine("Fetching {0}...", apiName);

        _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
        _db.AAStatus.Add(new Status(apiName, 100));

        await _db.SaveChangesAsync();
    }

    public async Task FetchAnalyticInstoreOpinions()
    {
        var apiName = "/1/analytic/instore/opinions";
        Console.WriteLine("Fetching {0}...", apiName);

        _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
        _db.AAStatus.Add(new Status(apiName, 100));

        await _db.SaveChangesAsync();
    }

    public async Task FetchAnalyticInstoreGain()
    {
        var apiName = "/1/analytic/instore/gains";
        Console.WriteLine("Fetching {0}...", apiName);

        _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
        _db.AAStatus.Add(new Status(apiName, 100));

        await _db.SaveChangesAsync();
    }
}