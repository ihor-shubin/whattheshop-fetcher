using System.Diagnostics;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;
using WhatTheShop.ApiClient;
using WhatTheShop.DB;
using WhatTheShop.Models;

namespace WhatTheShop.Services;

public class AnalyticPassingService
{
    private readonly WhatTheShopApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public AnalyticPassingService(WhatTheShopApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchAnalyticPassingCount(DateTime startDate, DateTime endDate, PerformContext context)
    {
        var apiName = "/1/analytic/passing/count";
        context.WriteLine("Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticPassingCounts.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            context.WriteProgressBar((i + 1) / _zones.Count * 100);
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticPassingCount result;

            if (!_overwriteDb && _db.AnalyticPassingCounts.Find(zone.Id) != null)
            {
                context.WriteLine("\tReading from DB...");
                _db.AnalyticPassingCounts.Find(zone.Id);
            }
            else
            {
                context.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticPassingCount(zone.Id, startDate, endDate);

                _db.AnalyticPassingCounts.Add(result);


                await _db.SaveChangesAsync();
            }

            context.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticPassingCountDetails()
    {
        var apiName = "/1/analytic/passing/countdetails";
        Console.WriteLine("Fetching {0}...", apiName);

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


                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticPassingCountHourDetails()
    {
        var apiName = "/1/analytic/passing/counthourdetails";
        Console.WriteLine("Fetching {0}...", apiName);

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


                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }
}