using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WhatTheShop.DB;
using WhatTheShop.Models;
using WhatTheShop.Utils;

namespace WhatTheShop.Services;

public class AnalyticPasserbyService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public AnalyticPasserbyService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchAnalyticPasserbyCount()
    {
        var apiName = "/1/analytic/passerby/count";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticPasserbyCounts.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticPasserbyCount result;

            if (!_overwriteDb && _db.AnalyticPasserbyCounts.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticPasserbyCounts.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticPasserbyCount(zone.Id);

                _db.AnalyticPasserbyCounts.Add(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticPasserbyCountDetails()
    {
        var apiName = "/1/analytic/passerby/countdetails";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticPasserbyCountDetails.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticPasserbyCountDetails> result;

            if (!_overwriteDb && _db.AnalyticPasserbyCountDetails.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticPasserbyCountDetails.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticPasserbyCountDetails(zone.Id);

                _db.AnalyticPasserbyCountDetails.AddRange(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticPasserbyCountHour()
    {
        var apiName = "/1/analytic/passerby/counthour";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticPasserbyCountHours.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticPasserbyCountHour> result;

            if (!_overwriteDb && _db.AnalyticPasserbyCountHours.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticPasserbyCountHours.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticPasserbyHour(zone.Id);

                _db.AnalyticPasserbyCountHours.AddRange(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticPasserbyCountDay()
    {
        var apiName = "/1/analytic/passerby/counthourday";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticPasserbyCountDays.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticPasserbyCountDay> result;

            if (!_overwriteDb && _db.AnalyticPasserbyCountDays.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticPasserbyCountDays.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticPasserbyCountDay(zone.Id);

                //db.AnalyticPasserbyCountDays.AddRange(result);
                //
                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticPasserbyCountSum()
    {
        var apiName = "/1/analytic/passerby/countsum";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticPasserbyCountSums.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticPasserbyCountSum> result;

            if (!_overwriteDb && _db.AnalyticPasserbyCountSums.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticPasserbyCountSums.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticPasserbyCountSum(zone.Id);

                _db.AnalyticPasserbyCountSums.AddRange(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticPasserbyBestTimes()
    {
        var apiName = "/1/analytic/passerby/besttimes";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticPasserbyBestTimes.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticPasserbyBestTimes result;

            if (!_overwriteDb && _db.AnalyticPasserbyBestTimes.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticPasserbyBestTimes.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticPasserbyBestTimes(zone.Id);

                _db.AnalyticPasserbyBestTimes.Add(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticPasserbyCountCommons()
    {
        var apiName = "/1/analytic/passerby/countcommon";
        Console.WriteLine($"Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.AnalyticPasserbyCountCommons.ExecuteDeleteAsync();
        }

        for (var i = 5; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticPasserbyCountCommon result;

            if (!_overwriteDb && _db.AnalyticPasserbyCountCommons.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticPasserbyCountCommons.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticPasserbyCountCommons(zone.Id);

                _db.AnalyticPasserbyCountCommons.Add(result);

                _db.AAStatus.RemoveIfExists(_db.AAStatus.Find(apiName));
                _db.AAStatus.Add(new Status(apiName, (i + 1.0) / _zones.Count * 100));

                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }
}