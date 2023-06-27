using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WhatTheShop.DB;
using WhatTheShop.Models;

namespace WhatTheShop.Services;

public class AnalyticDeviceService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private readonly List<Zone> _zones;

    public AnalyticDeviceService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;
        _zones = db.Zones.ToList();
    }

    public async Task FetchAnalyticDeviceCount()
    {
        Console.WriteLine("Fetching /1/analytic/devices/coun...");

        if (_overwriteDb)
        {
            await _db.AnalyticDeviceCount.ExecuteDeleteAsync();
        }

        for (var i = 0; i < _zones.Count; i++)
        {
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            AnalyticDeviceCount result;

            if (!_overwriteDb && _db.AnalyticDeviceCount.Find(zone.Id) != null)
            {
                Console.WriteLine("\tReading from DB...");
                result = _db.AnalyticDeviceCount.Find(zone.Id);
            }
            else
            {
                Console.WriteLine("\tReading from API...");
                result = await _worker.GetAnalyticDeviceCount(zone.Id);

                _db.AnalyticDeviceCount.Add(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }
}