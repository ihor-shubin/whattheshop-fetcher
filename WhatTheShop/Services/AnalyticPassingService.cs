using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WhatTheShop.DB;
using WhatTheShop.Models;
using WhatTheShop.Utils;

namespace WhatTheShop.Services;

public class AnalyticPassingService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public AnalyticPassingService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchAnalyticPassingCount()
    {
        var apiName = "/1/analytic/passing/count";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticPassingCounts.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticPassingCount result;

            if (!_overwriteDb && _db.AnalyticPassingCounts.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                _db.AnalyticPassingCounts.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticPassingCount(zone.Id);

                _db.AnalyticPassingCounts.Add(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticPassingCountDetails()
    {
        var apiName = "/1/analytic/passing/countdetails";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticPassingCountDetails.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticPassingCountDetails> result;

            if (!_overwriteDb && _db.AnalyticPassingCountDetails.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                _db.AnalyticPassingCountDetails.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticPassingCountDetails(zone.Id);

                _db.AnalyticPassingCountDetails.AddRange(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticPassingCountHourDetails()
    {
        var apiName = "/1/analytic/passing/counthourdetails";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticPassingCountHourDetails.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticPassingCountHourDetails> result;

            if (!_overwriteDb && _db.AnalyticPassingCountHourDetails.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                _db.AnalyticPassingCountHourDetails.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticPassingCountHourDetails(zone.Id);

                _db.AnalyticPassingCountHourDetails.AddRange(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }
}