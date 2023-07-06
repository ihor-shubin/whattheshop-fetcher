using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatTheShop.Models;

public interface IZoneEntity
{
    string ZoneId { get; init; }
}

public record Zone([property: Key] string Id, string Type, string Name, string Fullname);

public record ZoneInfo([property: Key] string Id, string Type, string Name, string Fullname, string Address, string ZipCode, string City, string Country, string Lat, string Lon, string Children, string Sensors);

public record Device([property: Key] string Name, int Ts, int MaxDuration, string CryptTokens);

public record Monitoring([property: Key] string ZoneId, string ZoneName, int DevicesOn, int DevicesTotal, int SensorsTotal) : IZoneEntity;

public record AnalyticPasserbyCount([property: Key] string ZoneId, int New, int Low, int Frequent, int Total) : IZoneEntity;

public record AnalyticPasserbyCountDetails([property: Key] Guid Id, string ZoneId, string Date, int New, int Low, int Frequent, int Total) : IZoneEntity;

public record AnalyticPasserbyCountHour([property: Key] Guid Id, string ZoneId, string Hour, double Count) : IZoneEntity;

public record AnalyticPasserbyCountDay([property: Key] Guid Id, string ZoneId, string Day, string Hour, int Count) : IZoneEntity;

public record AnalyticPasserbyCountSum([property: Key] Guid Id, string ZoneId, string Day, double Sum) : IZoneEntity;

public record AnalyticPasserbyBestTimes([property: Key] string ZoneId, string BestStartTime, string BestEndTime, string PeakStartTime, string PeakEndTime) : IZoneEntity;

public record AnalyticPasserbyCountCommon([property: Key] string ZoneId, double Common) : IZoneEntity;

public record AnalyticPassingCount([property: Key] string ZoneId, int Total) : IZoneEntity;

public record AnalyticPassingCountDetails([property: Key] Guid Id, string ZoneId, string Date, int All) : IZoneEntity;

public record AnalyticPassingCountHourDetails([property: Key] Guid Id, string ZoneId, string Hour, int All) : IZoneEntity;

public record AnalyticVisitorCount([property: Key] string ZoneId, int New, int Low, int Frequent, int Total) : IZoneEntity;

public record AnalyticVisitorCountDetails([property: Key] Guid Id, string ZoneId, string Date, int New, int Low, int Frequent, int All) : IZoneEntity;

public record AnalyticVisitorDuration([property: Key] Guid Id, string ZoneId, string Date, double Duration) : IZoneEntity;

public record AnalyticVisitorDurationDetails([property: Key] Guid Id, string ZoneId, string Date, double Duration0, double Duration300, double Duration900, double Duration1800) : IZoneEntity;

public record AnalyticVisitorBestTimes([property: Key] string ZoneId, string BestStartTime, string BestEndTime, string PeakStartTime, string PeakEndTime) : IZoneEntity;

public record AnalyticVisitorCountHour([property: Key] Guid Id, string ZoneId, string Hour, double Count) : IZoneEntity;

public record AnalyticVisitorCountHourDay([property: Key] Guid Id, string ZoneId, string Day, string Hour, int Count) : IZoneEntity;

public record AnalyticVisitorCountHourDayDetails([property: Key] Guid Id, string ZoneId, string Date, double Count) : IZoneEntity;

public record AnalyticVisitorCountHourDayStart([property: Key] Guid Id, string ZoneId, string Date, int Count) : IZoneEntity;

public record AnalyticVisitorCountSum([property: Key] Guid Id, string ZoneId, string Hour, double Count) : IZoneEntity;

public record AnalyticVisitorCountCommon([property: Key] string ZoneId, double? Count) : IZoneEntity;

public record AnalyticDeviceCount([property: Key] string ZoneId, double? Android, double? Ios) : IZoneEntity;

public record AnalyticZonesGeneral([property: Key] string ZoneId, int Percentage, string? Cnt, int Average, string Min, string Max) : IZoneEntity;

public record AnalyticZonesVenn([property: Key] string ZoneId, double? Count) : IZoneEntity;

public record CampaignList(string ZoneId, string ZoneName, List<string> Campaigns) : IZoneEntity; // need work

public record CampaignCount(string ZoneId, string ZoneName, string CampaignId, int Total, int Confirmed, int Target, int TargetDuration) : IZoneEntity; // need work

public record MediaVisitCount([property: Key] string ZoneId, int Facebook, int Google, int Teemo) : IZoneEntity;

public record MediaVisitCountDetails([property: Key] Guid Id, string ZoneId, string Date, int Facebook, int Google, int Teemo) : IZoneEntity;

public record AnalyticSensorCount([property: Key] string ZoneId, double? PresenceValue, double? PresenceMaxValue, string? PresenceLastUpdate, double? InValue, double? OutValue, double? AbsoluteValue) : IZoneEntity;

public record AnalyticSensorCountDetails([property: Key] Guid Id, string ZoneId, string Date, double? PresenceValue, double? InValue, double? OutValue, double? AbsoluteValue) : IZoneEntity;

public record AnalyticSystemLastUpdate([property: Key] string ZoneId, string? Date, [property: Column("NeedUpdate")] bool? NeedUpdate) : IZoneEntity;

public record AnalyticSystemQuickLastUpdate([property: Key] string ZoneId, string? Date, [property: Column("NeedUpdate")] bool? NeedUpdate) : IZoneEntity;

public record AnalyticSystemForceRefresh([property: Key] string ZoneId, bool? Result) : IZoneEntity;

public record AnalyticSystemTemporaryTable([property: Key] string ZoneId, string? Context, string? Table) : IZoneEntity;

public record AnalyticRawPasserby([property: Key] Guid Id, string ZoneId, string Uid, string UserMac, string DateStart, string DateEnd, string IsLocal) : IZoneEntity;

public record AnalyticRawServiceVisitor([property: Key] Guid Id, string ZoneId, string Uid, string UserMac, string DateStart, string DateEnd, string IsLocal, string CntVisit, string Zones, string DeviceType, string Frequency, double MaxProximity) : IZoneEntity;

public record AnalyticRawServiceVisitorLight([property: Key] Guid Id, string ZoneId, string Uid, string DateDay, string DateStart, string DateEnd) : IZoneEntity;

public record AnalyticRawServiceVisitorMacList([property: Key] Guid Id, string ZoneId, string UserMac) : IZoneEntity;

public record AnalyticRawServicePasserbyMacList([property: Key] Guid Id, string ZoneId, string UserMac) : IZoneEntity;

public record AnalyticVisitCount([property: Key] string ZoneId, int Total) : IZoneEntity;

public record AnalyticVisitCountDetails([property: Key] Guid Id, string ZoneId, string Date, int All) : IZoneEntity;

public record AnalyticVisitCountHourDetails([property: Key] Guid Id, string ZoneId, string Hour, int All) : IZoneEntity;

public record AnalyticVisitDuration([property: Key] Guid Id, string ZoneId, string Date, double Duration) : IZoneEntity;

public record AnalyticVisitDurationDetails([property: Key] Guid Id, string ZoneId, string Date, double Duration0, double Duration300, double Duration900, double Duration1800) : IZoneEntity;

public record AnalyticVisitCountHour([property: Key] Guid Id, string ZoneId, string Hour, double Count) : IZoneEntity;

public record AnalyticVisitCountHourDay([property: Key] Guid Id, string ZoneId, string Day, string Hour, int Count) : IZoneEntity;

public record AnalyticVisitCountHourDayStart([property: Key] Guid Id, string ZoneId, string Date, int Count) : IZoneEntity;
