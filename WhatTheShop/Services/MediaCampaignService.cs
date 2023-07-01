using WhatTheShop.DB;
using WhatTheShop.Models;
using WhatTheShop.Utils;

namespace WhatTheShop.Services;

public class MediaCampaignService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public MediaCampaignService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchMediaCampaignList()
    {
        var apiName = "/1/media/campaign/list";
        
        Console.WriteLine("Fetching {0}...", apiName);

        // always empty
        var result = await _worker.GetCampaignList(_zones);
        
        _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
        _db.AAStatus.Add(new Status(apiName, 100));
    }

    public async Task FetchMediaCampaignCount()
    {
        var apiName = "/1/media/campaign/count";
        Console.WriteLine("Fetching {0}...", apiName);

        // requires campaigns that are always empty, so it's always empty for now

        var campaignList = new List<CampaignList>();
        var result = await _worker.GetCampaignCount(campaignList);

        _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
        _db.AAStatus.Add(new Status(apiName, 100));
    }
}