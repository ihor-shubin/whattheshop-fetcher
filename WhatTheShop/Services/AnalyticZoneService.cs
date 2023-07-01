using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WhatTheShop.DB;
using WhatTheShop.Models;
using WhatTheShop.Utils;

namespace WhatTheShop.Services;

public class AnalyticZoneService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public AnalyticZoneService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchAnalyticZonesGeneral()
    {
        var apiName = "/1/analytic/zone/general";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticZonesGeneral.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticZonesGeneral result;

            if (!_overwriteDb && _db.AnalyticZonesGeneral.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticZonesGeneral.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticZonesGeneral(zone.Id);

                _db.AnalyticZonesGeneral.Add(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticZonesVenn()
    {
        var apiName = "/1/analytic/zone/venn";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticZonesVenn.ExecuteDeleteAsync();
        }

        var sw = new Stopwatch();
        sw.Start();

        List<AnalyticZonesVenn> result;

        if (_overwriteDb && _db.AnalyticZonesVenn.Any())
        {
            Console.WriteLine("\tReading from DB...");
            result = _db.AnalyticZonesVenn.ToList();
        }
        else
        {
            Console.WriteLine("\tReading from API...");
            result = await _worker.GetAnalyticZonesVenn(_zones);

            _db.AnalyticZonesVenn.AddRange(result);

            _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
            _db.AAStatus.Add(new Status(apiName, 100));

            await _db.SaveChangesAsync();
        }

        Console.WriteLine($"\tProcessed in {sw.Elapsed}");
    }

    public async Task FetchAnalyticZonesSankey()
    {
        var apiName = "/1/analytic/zone/sankey";

        Console.WriteLine($"Fetching {0}...", apiName);


        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            Console.WriteLine("\tReading from API...");
            var result = await _worker.GetAnalyticZonesSankey(zone.Id);


            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }
}