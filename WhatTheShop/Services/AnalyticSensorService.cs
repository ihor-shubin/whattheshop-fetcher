using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WhatTheShop.DB;
using WhatTheShop.Models;
using WhatTheShop.Utils;

namespace WhatTheShop.Services;

public class AnalyticSensorService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public AnalyticSensorService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchAnalyticSensorCount()
    {
        var apiName = "/1/analytic/sensor/count";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticSensorCount.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticSensorCount result;

            if (!_overwriteDb && _db.AnalyticSensorCount.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticSensorCount.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticSensorCount(zone.Id);

                _db.AnalyticSensorCount.Add(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticSensorCountDetails()
    {
        var apiName = "/1/analytic/sensor/countdetails";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticSensorCountDetails.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticSensorCountDetails> result;

            if (_overwriteDb && _db.AnalyticSensorCountDetails.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticSensorCountDetails.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticSensorCountDetails(zone.Id);

                _db.AnalyticSensorCountDetails.AddRange(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticSensorCountRaw()
    {
        var apiName = " /1/analytic/sensor/countRaw...";

        Console.WriteLine($"Fetching {0}...", apiName);
        // it requires sensor or hardware id. It's not available in the API
        var result = await _worker.GetAnalyticSensorCountRaw(string.Empty);
    }

    public async Task FetchAnalyticSensorCountDetailsRaw()
    {
        var apiName = "tbd";
        Console.WriteLine($"Fetching {0}...", apiName);

        await _db.SaveChangesAsync();
        // it requires sensor or hardware id. It's not available in the API
    }
}