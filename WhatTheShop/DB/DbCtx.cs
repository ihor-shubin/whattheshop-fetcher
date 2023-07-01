using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using WhatTheShop.Models;

namespace WhatTheShop.DB;

public class DbCtx: DbContext
{
    public DbSet<Zone> Zones { get; set; } = null!;

    public DbSet<Device> Devices { get; set; } = null!;

    public DbSet<ZoneInfo> ZonesInfos { get; set; } = null!;

    public DbSet<Monitoring> Monitorings { get; set; } = null!;
    
    public DbSet<AnalyticPasserbyCount> AnalyticPasserbyCounts { get; set; } = null!;

    public DbSet<AnalyticPasserbyCountDetails> AnalyticPasserbyCountDetails { get; set; } = null!;

    public DbSet<AnalyticPasserbyCountHour> AnalyticPasserbyCountHours { get; set; } = null!;
    
    public DbSet<AnalyticPasserbyCountDay> AnalyticPasserbyCountDays { get; set; } = null!;

    public DbSet<AnalyticPasserbyCountSum> AnalyticPasserbyCountSums { get; set; } = null!;
    
    public DbSet<AnalyticPasserbyBestTimes> AnalyticPasserbyBestTimes { get; set; } = null!;

    public DbSet<AnalyticPasserbyCountCommon> AnalyticPasserbyCountCommons { get; set; } = null!;
    
    public DbSet<AnalyticPassingCount> AnalyticPassingCounts { get; set; } = null!;
    
    public DbSet<AnalyticPassingCountDetails> AnalyticPassingCountDetails { get; set; } = null!;

    public DbSet<AnalyticPassingCountHourDetails> AnalyticPassingCountHourDetails { get; set; } = null!;
    
    public DbSet<AnalyticVisitorCount> AnalyticVisitorCounts { get; set; } = null!;
    
    public DbSet<AnalyticVisitorCountDetails> AnalyticVisitorCountDetails { get; set; } = null!;
    
    public DbSet<AnalyticVisitorDuration> AnalyticVisitorDurations { get; set; } = null!;
    
    public DbSet<AnalyticVisitorDurationDetails> AnalyticVisitorDurationDetails { get; set; } = null!;

    public DbSet<AnalyticVisitorBestTimes> AnalyticVisitorBestTimes { get; set; } = null!;
    
    public DbSet<AnalyticVisitorCountHour> AnalyticVisitorCountHours { get; set; } = null!;
    
    public DbSet<AnalyticVisitorCountHourDay> AnalyticVisitorCountHourDays { get; set; } = null!;
    
    public DbSet<AnalyticVisitorCountHourDayDetails> AnalyticVisitorCountHourDayDetails { get; set; } = null!;
    
    public DbSet<AnalyticVisitorCountHourDayStart> AnalyticVisitorCountHourDayStart { get; set; } = null!;

    public DbSet<AnalyticVisitorCountSum> AnalyticVisitorCountSum { get; set; } = null!;
    
    public DbSet<AnalyticVisitorCountCommon> AnalyticVisitorCountCommon { get; set; } = null!;
    
    public DbSet<AnalyticDeviceCount> AnalyticDeviceCount { get; set; } = null!;
    
    public DbSet<AnalyticZonesGeneral> AnalyticZonesGeneral { get; set; } = null!;
    
    public DbSet<AnalyticZonesVenn> AnalyticZonesVenn { get; set; } = null!;
    
    public DbSet<MediaVisitCount> MediaVisitCount { get; set; } = null!;

    public DbSet<MediaVisitCountDetails> MediaVisitCountDetails { get; set; } = null!;
    
    public DbSet<AnalyticSensorCount> AnalyticSensorCount { get; set; } = null!;
    
    public DbSet<AnalyticSensorCountDetails> AnalyticSensorCountDetails { get; set; } = null!;
    
    public DbSet<AnalyticSystemLastUpdate> AnalyticSystemLastUpdate { get; set; } = null!;
    
    public DbSet<AnalyticSystemQuickLastUpdate> AnalyticSystemQuickLastUpdate { get; set; } = null!;
    
    public DbSet<AnalyticSystemForceRefresh> AnalyticSystemForceRefresh { get; set; } = null!;
    
    public DbSet<AnalyticSystemTemporaryTable> AnalyticSystemTemporaryTable { get; set; } = null!;
    
    public DbSet<AnalyticRawPasserby> AnalyticRawPasserby { get; set; } = null!;
    
    public DbSet<AnalyticRawServiceVisitor> AnalyticRawServiceVisitor { get; set; } = null!;
    
    public DbSet<AnalyticRawServiceVisitorLight> AnalyticRawServiceVisitorLight { get; set; } = null!;
    
    public DbSet<AnalyticRawServiceVisitorMacList> AnalyticRawServiceVisitorMacList { get; set; } = null!;
    
    public DbSet<AnalyticRawServicePasserbyMacList> AnalyticRawServicePasserbyMacList { get; set; } = null!;
    
    public DbSet<AnalyticVisitCount> AnalyticVisitCount { get; set; } = null!;
    
    public DbSet<AnalyticVisitCountDetails> AnalyticVisitCountDetails { get; set; } = null!;

    public DbSet<AnalyticVisitCountHourDetails> AnalyticVisitCountHourDetails { get; set; } = null!;
    
    public DbSet<AnalyticVisitDuration> AnalyticVisitDuration { get; set; } = null!;

    public DbSet<AnalyticVisitDurationDetails> AnalyticVisitDurationDetails { get; set; } = null!;
    
    public DbSet<AnalyticVisitCountHour> AnalyticVisitCountHour { get; set; } = null!;
    
    public DbSet<AnalyticVisitCountHourDay> AnalyticVisitCountHourDay { get; set; } = null!;

    public DbSet<AnalyticVisitCountHourDayStart> AnalyticVisitCountHourDayStart { get; set; } = null!;
    
    public DbSet<Status> AAStatus { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "WhatTheShop_dump.sqlite")}");
}