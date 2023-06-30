using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WhatTheShop.DB;
using WhatTheShop.Models;

namespace WhatTheShop.Services;

public class AnalyticSystemService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public AnalyticSystemService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }


    public async Task FetchAnalyticSystemLastUpdate()
    {
        Console.WriteLine("Fetching /1/analytic/system/lastupdate...");

        if (_overwriteDb)
        {
            await _db.AnalyticSystemLastUpdate.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticSystemLastUpdate? result;

            if (!_overwriteDb && _db.AnalyticSystemLastUpdate.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticSystemLastUpdate.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticSystemLastUpdate(zone.Id);

                _db.AnalyticSystemLastUpdate.Add(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticSystemQuickLastUpdate()
    {
        Console.WriteLine("Fetching /1/analytic/system/quicklastupdate...");

        if (_overwriteDb)
        {
            await _db.AnalyticSystemQuickLastUpdate.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticSystemQuickLastUpdate result;

            if (!_overwriteDb && _db.AnalyticSystemQuickLastUpdate.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticSystemQuickLastUpdate.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticSystemQuickLastUpdate(zone.Id);

                _db.AnalyticSystemQuickLastUpdate.Add(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticSystemForceRefresh()
    {
        Console.WriteLine("Fetching /1/analytic/system/quicklastupdate...");

        if (_overwriteDb)
        {
            await _db.AnalyticSystemForceRefresh.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticSystemForceRefresh result;

            if (!_overwriteDb && _db.AnalyticSystemForceRefresh.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticSystemForceRefresh.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticSystemForceRefresh(zone.Id);

                _db.AnalyticSystemForceRefresh.Add(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticSystemTemporaryTable()
    {
        Console.WriteLine("Fetching /1/analytic/system/temporaryTable...");

        if (_overwriteDb)
        {
            await _db.AnalyticSystemTemporaryTable.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticSystemTemporaryTable result;

            if (!_overwriteDb && _db.AnalyticSystemTemporaryTable.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticSystemTemporaryTable.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticSystemTemporaryTable(zone.Id);

                _db.AnalyticSystemTemporaryTable.Add(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }
}