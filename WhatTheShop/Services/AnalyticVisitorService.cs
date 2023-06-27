using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WhatTheShop.DB;
using WhatTheShop.Models;

namespace WhatTheShop.Services;

public class AnalyticVisitorService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public AnalyticVisitorService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchAnalyticVisitorCount()
    {
        Console.WriteLine("Fetching /1/analytic/visitor/count...");

        if (_overwriteDb)
        {
            await _db.AnalyticVisitorCounts.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticVisitorCount result;

            if (!_overwriteDb && _db.AnalyticVisitorCounts.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticVisitorCounts.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticVisitorCount(zone.Id);

                _db.AnalyticVisitorCounts.Add(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticVisitorCountDetails()
    {
        Console.WriteLine("Fetching /1/analytic/visitor/countdetails...");

        if (_overwriteDb)
        {
            await _db.AnalyticVisitorCountDetails.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticVisitorCountDetails> result;

            if (!_overwriteDb && _db.AnalyticVisitorCountDetails.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticVisitorCountDetails.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticVisitorCountDetails(zone.Id);

                _db.AnalyticVisitorCountDetails.AddRange(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticVisitorDuration()
    {
        Console.WriteLine("Fetching /1/analytic/visitor/duration...");

        if (_overwriteDb)
        {
            await _db.AnalyticVisitorDurations.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticVisitorDuration> result;

            if (!_overwriteDb && _db.AnalyticVisitorDurations.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticVisitorDurations.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticVisitorDurations(zone.Id);

                _db.AnalyticVisitorDurations.AddRange(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticVisitorDurationDetails()
    {
        Console.WriteLine("Fetching /1/analytic/visitor/durationdetails...");

        if (_overwriteDb)
        {
            await _db.AnalyticVisitorDurationDetails.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticVisitorDurationDetails> result;

            if (!_overwriteDb && _db.AnalyticVisitorDurationDetails.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticVisitorDurationDetails.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticVisitorDurationDetails(zone.Id);

                _db.AnalyticVisitorDurationDetails.AddRange(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticVisitorBestTimes()
    {
        Console.WriteLine("Fetching /1/analytic/visitor/besttimes...");

        if (_overwriteDb)
        {
            await _db.AnalyticVisitorBestTimes.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticVisitorBestTimes result;

            if (!_overwriteDb && _db.AnalyticVisitorBestTimes.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticVisitorBestTimes.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticVisitorBestTimes(zone.Id);

                _db.AnalyticVisitorBestTimes.Add(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticVisitorCountHour()
    {
        Console.WriteLine("Fetching /1/analytic/visitor/counthour...");

        if (_overwriteDb)
        {
            await _db.AnalyticVisitorCountHours.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticVisitorCountHour> result;

            if (!_overwriteDb && _db.AnalyticVisitorCountHours.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticVisitorCountHours.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetVisitorPasserbyHour(zone.Id);

                _db.AnalyticVisitorCountHours.AddRange(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticVisitorCountHourDay()
    {
        Console.WriteLine("Fetching /1/analytic/visitor/counthourday...");

        if (_overwriteDb)
        {
            await _db.AnalyticVisitorCountHourDays.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticVisitorCountHourDay> result;

            if (!_overwriteDb && _db.AnalyticVisitorCountHourDays.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticVisitorCountHourDays.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticVisitorCountHourDay(zone.Id);

                _db.AnalyticVisitorCountHourDays.AddRange(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticVisitorCountHourDayDetails()
    {
        Console.WriteLine("Fetching /1/analytic/visitor/counthourdetails...");

        if (_overwriteDb)
        {
            await _db.AnalyticVisitorCountHourDayDetails.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticVisitorCountHourDayDetails> result;

            if (!_overwriteDb && _db.AnalyticVisitorCountHourDayDetails.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticVisitorCountHourDayDetails.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticVisitorCountHourDayDetails(zone.Id);

                _db.AnalyticVisitorCountHourDayDetails.AddRange(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticVisitorCountHourDayStart()
    {
        Console.WriteLine("Fetching /1/analytic/visitor/counthourdaystart...");

        if (_overwriteDb)
        {
            await _db.AnalyticVisitorCountHourDayDetails.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticVisitorCountHourDayStart> result;

            if (!_overwriteDb && _db.AnalyticVisitorCountHourDayStart.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticVisitorCountHourDayStart.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticVisitorCountHourDayStart(zone.Id);

                _db.AnalyticVisitorCountHourDayStart.AddRange(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticVisitorCountSum()
    {
        Console.WriteLine("Fetching /1/analytic/visitor/countsum...");

        if (_overwriteDb)
        {
            await _db.AnalyticVisitorCountSum.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<AnalyticVisitorCountSum> result;

            if (!_overwriteDb && _db.AnalyticVisitorCountSum.Any(x => x.ZoneId == zone.Id))
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticVisitorCountSum.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticVisitorCountSum(zone.Id);

                _db.AnalyticVisitorCountSum.AddRange(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticVisitorCountCommon()
    {
        Console.WriteLine("Fetching /1/analytic/visitor/countcommon...");

        if (_overwriteDb)
        {
            await _db.AnalyticVisitorCountCommon.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticVisitorCountCommon result;

            if (!_overwriteDb && _db.AnalyticVisitorCountCommon.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticVisitorCountCommon.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticVisitorCountCommon(zone.Id);

                _db.AnalyticVisitorCountCommon.Add(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }
}