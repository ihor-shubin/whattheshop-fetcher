using Microsoft.EntityFrameworkCore;
using WhatTheShop.ApiClient;
using WhatTheShop.DB;
using WhatTheShop.Models;

namespace WhatTheShop.Services;

public class UserService
{
    private readonly WhatTheShopApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private List<Zone> _zones = new();

    public UserService(WhatTheShopApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;

        FetchZones().Wait();
    }

    public async Task FetchZones()
    {
        var apiName = "/1/user/zones";
        Console.WriteLine("Fetching {0}...", apiName);

        if (_overwriteDb)
        {
            await _db.Zones.ExecuteDeleteAsync();
        }

        if (_db.Zones.Any())
        {
            Console.WriteLine("\tReading from DB...");
            _zones = _db.Zones.ToList();
        }
        else
        {
            Console.WriteLine("\tReading from API...");
            _zones = await _worker.GetZones();

            _db.Zones.AddRange(_zones);


            await _db.SaveChangesAsync();
        }
    }

    public async Task FetchZoneInfos()
    {
        var apiName = "1/user/zoneinfos";

        Console.WriteLine("Fetching {0}...", apiName);

        List<ZoneInfo> zoneInfos;

        if (_overwriteDb)
        {
            await _db.ZonesInfos.ExecuteDeleteAsync();
        }

        if (!_overwriteDb && _db.ZonesInfos.Any())
        {
            Console.WriteLine("\tReading fromDB...");
            zoneInfos = _db.ZonesInfos.ToList();
        }
        else
        {
            Console.WriteLine("\tReading fromAPI...");
            zoneInfos = await _worker.GetZonesInfo(_zones);

            await _db.ZonesInfos.ExecuteDeleteAsync();
            _db.ZonesInfos.AddRange(zoneInfos);


            await _db.SaveChangesAsync();
        }
    }

    public async Task FetchDevices()
    {
        var apiName = "/1/user/devices";

        Console.WriteLine("Fetching {0}...", apiName);
        List<Device> devices;

        if (_overwriteDb)
        {
            await _db.Devices.ExecuteDeleteAsync();
        }

        if (_db.Devices.Any())
        {
            Console.WriteLine("\tReading fromDB...");
            devices = _db.Devices.ToList();
        }
        else
        {
            Console.WriteLine("\tReading fromAPI...");
            devices = await _worker.GetDevices();

            await _db.Devices.ExecuteDeleteAsync();
            _db.Devices.AddRange(devices);


            await _db.SaveChangesAsync();
        }
    }

    public async Task FetchMonitoring()
    {
        var apiName = "/1/user/monitoring";
        Console.WriteLine("Fetching {0}...", apiName);

        List<Monitoring> monitorings;

        if (_overwriteDb)
        {
            await _db.Monitorings.ExecuteDeleteAsync();
        }

        if (_db.Monitorings.Any())
        {
            Console.WriteLine("\tReading fromDB...");
            monitorings = _db.Monitorings.ToList();
        }
        else
        {
            Console.WriteLine("\tReading fromAPI...");
            monitorings = await _worker.GetMonitoring(_zones);

            await _db.Monitorings.ExecuteDeleteAsync();
            _db.Monitorings.AddRange(monitorings);


            await _db.SaveChangesAsync();
        }
    }
}