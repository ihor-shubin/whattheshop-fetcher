using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WhatTheShop.DB;
using WhatTheShop.Models;
using WhatTheShop.Utils;

namespace WhatTheShop.Services;

public class AnalyticRawService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public AnalyticRawService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchAnalyticRawServicePasserby()
    {
        var apiName = "/1/analytic/raw/passerby";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticRawPasserby.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticRawPasserby> result;

            if (!_overwriteDb && _db.AnalyticRawPasserby.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticRawPasserby.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticRawServicePasserby(zone.Id);

                _db.AnalyticRawPasserby.AddRange(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticRawServiceVisitor()
    {
        var apiName = "/1/analytic/raw/visitor";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticRawServiceVisitor.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticRawServiceVisitor> result;

            if (!_overwriteDb && _db.AnalyticRawServiceVisitor.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticRawServiceVisitor.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticRawServiceVisitor(zone.Id);

                _db.AnalyticRawServiceVisitor.AddRange(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticRawServiceVisitorLight()
    {
        var apiName = "/1/analytic/raw/visitorLight";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticRawServiceVisitorLight.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticRawServiceVisitorLight> result;

            if (!_overwriteDb && _db.AnalyticRawServiceVisitorLight.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticRawServiceVisitorLight.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticRawServiceVisitorLight(zone.Id);

                _db.AnalyticRawServiceVisitorLight.AddRange(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticRawServiceVisitorMacList()
    {
        var apiName = "/1/analytic/raw/visitorMacList";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticRawServiceVisitorMacList.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticRawServiceVisitorMacList> result;

            if (!_overwriteDb && _db.AnalyticRawServiceVisitorMacList.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticRawServiceVisitorMacList.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticRawServiceVisitorMacList(zone.Id);

                _db.AnalyticRawServiceVisitorMacList.AddRange(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticRawServicePasserbyMacList()
    {
        var apiName = "/1/analytic/raw/passerbyMacList";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticRawServicePasserbyMacList.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticRawServicePasserbyMacList> result;

            if (!_overwriteDb && _db.AnalyticRawServicePasserbyMacList.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticRawServicePasserbyMacList.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticRawServicePasserbyMacList(zone.Id);

                _db.AnalyticRawServicePasserbyMacList.AddRange(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }
}