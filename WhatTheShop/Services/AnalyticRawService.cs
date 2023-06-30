using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WhatTheShop.DB;
using WhatTheShop.Models;

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
        Console.WriteLine("Fetching /1/analytic/raw/passerby...");

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
                result = await _worker.GetAnalyticRawServicePasserby(zone.Id, _zones);

                _db.AnalyticRawPasserby.AddRange(result);
                await _db.SaveChangesAsync();
            }

            Console.WriteLine($"\tProcessed {i + 1} of {_zones.Count} in {sw.Elapsed}");
        }
    }

    public async Task FetchAnalyticRawServiceVisitor()
    {
        throw new NotImplementedException();
    }

    public async Task FetchAnalyticRawServiceVisitorLight()
    {
        throw new NotImplementedException();
    }

    public async Task FetchAnalyticRawServiceVisitorMacList()
    {
        throw new NotImplementedException();
    }

    public async Task FetchAnalyticRawServicePasserbyMacList()
    {
        throw new NotImplementedException();
    }
}