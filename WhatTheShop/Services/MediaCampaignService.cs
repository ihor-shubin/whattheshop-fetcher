using WhatTheShop.ApiClient;
using WhatTheShop.DB;
using WhatTheShop.Models;

namespace WhatTheShop.Services;

public class MediaCampaignService
{
    private readonly WhatTheShopApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public MediaCampaignService(WhatTheShopApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    // always empty
    public async Task FetchMediaCampaignList()
    {
        var apiName = "/1/media/campaign/list";

        Console.WriteLine("Fetching {0}...", apiName);

        // always empty
        var result = await _worker.GetCampaignList(_zones);
    }
    // missing data from FetchMediaCampaignList()
    public async Task FetchMediaCampaignCount()
    {
        var apiName = "/1/media/campaign/count";
        Console.WriteLine("Fetching {0}...", apiName);

        // requires campaigns that are always empty, so it's always empty for now

        var campaignList = new List<CampaignList>();
        var result = await _worker.GetCampaignCount(campaignList);
    }
}