using System.Diagnostics;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;
using WhatTheShop.DB;
using WhatTheShop.Models;

namespace WhatTheShop.Services;

public abstract class BaseService
{
    protected readonly DbCtx Db;
    private readonly List<Zone> _zones;

    protected BaseService(DbCtx db)
    {
        Db = db;
        _zones = db.Zones.ToList();
    }

    protected async Task RunForMultiResponse<T>(
        DateTime start,
        DateTime end,
        
        PerformContext context,
        
        string apiName,
        
        DbSet<T> dbModel,
        
        Func<string, DateTime, DateTime, Task<List<T>>> apiDataFetcher) 
        where T : class, IZoneEntity
    {
        context.WriteLine("Fetching {0}...", apiName);

        for (var i = 0; i < _zones.Count; i++)
        {
            context.WriteProgressBar("ZonesProgress", (i + 1.0) / _zones.Count * 100);
            var sw = new Stopwatch();
            sw.Start();

            var zone = _zones[i];
            List<T> result;

            if (dbModel.Any(x => x.ZoneId == zone.Id))
            {
                context.WriteLine("\tReading from DB...");
                result = dbModel.Where(x => x.ZoneId == zone.Id).ToList();
            }
            else
            {
                context.WriteLine("\tReading from API...");
                result = await apiDataFetcher(zone.Id, start, end);
                context.WriteLine($"\tFetched {result.Count} entities...");
                dbModel.AddRange(result);


                await Db.SaveChangesAsync();
            }

            context.WriteLine($"\tProcessed {i + 1} of {_zones.Count} zones in {sw.Elapsed}");
        }
    }
}