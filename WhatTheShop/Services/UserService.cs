using Microsoft.EntityFrameworkCore;
using WhatTheShop.DB;
using WhatTheShop.Models;

namespace WhatTheShop.Services;

public class UserService
{
    private readonly ApiClient _worker;
    private readonly DbCtx _db;
    private readonly bool _overwriteDb;
    private List<Zone> _zones = new();

    public UserService(ApiClient worker, DbCtx db, bool overwriteDb = false)
    {
        _worker = worker;
        _db = db;
        _overwriteDb = overwriteDb;

        FetchZones().Wait();
    }

    public async Task FetchZones()
    {
        Console.WriteLine("Fetching /1/user/zones...");

        if (!_overwriteDb && _db.Zones.Any())
        {
            Console.WriteLine("\tReading from DB...");
            _zones = _db.Zones.ToList();
        }
        else
        {
            Console.WriteLine("\tReading from API...");
            _zones = await _worker.GetZones();

            await _db.Zones.ExecuteDeleteAsync();
            _db.Zones.AddRange(_zones);
            await _db.SaveChangesAsync();
        }
    }

    public async Task FetchZoneInfos()
    {
        Console.WriteLine("Fetching 1/user/zoneinfos...");
        List<ZoneInfo> zoneInfos;

        if (!_overwriteDb && _db.ZonesInfos.Any())
        {
            Console.WriteLine("\tReading fromDB...");
            _db.ZonesInfos.ToList();
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
        Console.WriteLine("Fetching /1/user/devices...");
        List<Device> devices;

        if (!_overwriteDb && _db.Devices.Any())
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
        Console.WriteLine("Fetching /1/user/monitoring...");
        List<Monitoring> monitorings;

        if (!_overwriteDb && _db.Monitorings.Any())
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