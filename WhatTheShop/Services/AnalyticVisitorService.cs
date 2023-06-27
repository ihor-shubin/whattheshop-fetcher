﻿using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WhatTheShop.DB;

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

    public async Task FetchAnalyticVisitorCoutHourDay()
    {
        throw new NotImplementedException();
    }

    public async Task FetchAnalyticVisitorCoutHourDayDetails()
    {
        throw new NotImplementedException();
    }

    public async Task FetchAnalyticVisitorCountHourDayStart()
    {
        throw new NotImplementedException();
    }

    public async Task FetchAnalyticVisitorCountSum()
    {
        throw new NotImplementedException();
    }

    public async Task FetchAnalyticVisitorCountCommon()
    {
        throw new NotImplementedException();
    }
}