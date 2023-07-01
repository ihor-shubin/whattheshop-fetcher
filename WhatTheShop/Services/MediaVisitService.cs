using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WhatTheShop.DB;
using WhatTheShop.Models;
using WhatTheShop.Utils;

namespace WhatTheShop.Services;

public class MediaVisitService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public MediaVisitService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchMediaVisitCount()
    {
        var apiName = "/1/media/visit/count";
        Console.WriteLine("Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.MediaVisitCount.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            MediaVisitCount result;

            if (!_overwriteDb && _db.MediaVisitCount.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.MediaVisitCount.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetMediaVisitCount(zone.Id);

                _db.MediaVisitCount.Add(result);
                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchMediaVisitCountDetails()
    {
        var apiName = "/1/media/visit/countdetails";
        Console.WriteLine("Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.MediaVisitCountDetails.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<MediaVisitCountDetails> result;

            if (!_overwriteDb && _db.MediaVisitCountDetails.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.MediaVisitCountDetails.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetMediaVisitCountDetails(zone.Id);

                _db.MediaVisitCountDetails.AddRange(result);
                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }
}