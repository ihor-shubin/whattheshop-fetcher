using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatTheShop.Models;

public record Zone([property: Key] string Id, string Type, string Name, string Fullname);

public record ZoneInfo([property: Key] string Id, string Type, string Name, string Fullname, string Address, string ZipCode, string City, string Country, string Lat, string Lon, string Children, string Sensors);

public record Device([property: Key] string Name, int Ts, int MaxDuration, string CryptTokens);

public record Monitoring([property: Key] string ZoneId, string ZoneName, int DevicesOn, int DevicesTotal, int SensorsTotal);

public record AnalyticPasserbyCount([property: Key] string ZoneId, int New, int Low, int Frequent, int Total);

public record AnalyticPasserbyCountDetails([property: Key] Guid Id, string ZoneId, string Date, int New, int Low, int Frequent, int Total);

public record AnalyticPasserbyCountHour([property: Key] Guid Id, string ZoneId, string Hour, double Count);

public record AnalyticPasserbyCountDay([property: Key] Guid Id, string ZoneId, string Day, string Hour, int Count);

public record AnalyticPasserbyCountSum([property: Key] Guid Id, string ZoneId, string Day, double Sum);

public record AnalyticPasserbyBestTimes([property: Key] string ZoneId, string BestStartTime, string BestEndTime, string PeakStartTime, string PeakEndTime);

public record AnalyticPasserbyCountCommon([property: Key] string ZoneId, double Common);

public record AnalyticPassingCount([property: Key] string ZoneId, int Total);

public record AnalyticPassingCountDetails([property: Key] Guid Id, string ZoneId, string Date, int All);

public record AnalyticPassingCountHourDetails([property: Key] Guid Id, string ZoneId, string Hour, int All);

public record AnalyticVisitorCount([property: Key] string ZoneId, int New, int Low, int Frequent, int Total);

public record AnalyticVisitorCountDetails([property: Key] Guid Id, string ZoneId, string Date, int New, int Low, int Frequent, int All);

public record AnalyticVisitorDuration([property: Key] Guid Id, string ZoneId, string Date, double Duration);

public record AnalyticVisitorDurationDetails([property: Key] Guid Id, string ZoneId, string Date, double Duration0, double Duration300, double Duration900, double Duration1800);

public record AnalyticVisitorBestTimes([property: Key] string ZoneId, string BestStartTime, string BestEndTime, string PeakStartTime, string PeakEndTime);

public record AnalyticVisitorCountHour([property: Key] Guid Id, string ZoneId, string Hour, double Count);

public record AnalyticVisitorCountHourDay([property: Key] Guid Id, string ZoneId, string Day, string Hour, int Count);

public record AnalyticVisitorCountHourDayDetails([property: Key] Guid Id, string ZoneId, string Date, double Count);

public record AnalyticVisitorCountHourDayStart([property: Key] Guid Id, string ZoneId, string Date, int Count);

public record AnalyticVisitorCountSum([property: Key] Guid Id, string ZoneId, string Hour, double Count);

public record AnalyticVisitorCountCommon([property: Key] string ZoneId, double? Count);

public record AnalyticDeviceCount([property: Key] string ZoneId, double? Android, double? Ios);

public record AnalyticZonesGeneral([property: Key] string ZoneId, int Percentage, string? Cnt, int Average, string Min, string Max);

public record AnalyticZonesVenn([property: Key] string ZoneId, double? Count);

public record CampaignList(string ZoneId, string ZoneName, List<string> Campaigns); // need work

public record CampaignCount(string ZoneId, string ZoneName, string CampaignId, int Total, int Confirmed, int Target, int TargetDuration); // need work

public record MediaVisitCount([property: Key] string ZoneId, int Facebook, int Google, int Teemo);

public record MediaVisitCountDetails([property: Key] Guid Id, string ZoneId, string Date, int Facebook, int Google, int Teemo);

public record AnalyticSensorCount([property: Key] string ZoneId, double? PresenceValue, double? PresenceMaxValue, string? PresenceLastUpdate, double? InValue, double? OutValue, double? AbsoluteValue);

public record AnalyticSensorCountDetails([property: Key] Guid Id, string ZoneId, string Date, double? PresenceValue, double? InValue, double? OutValue, double? AbsoluteValue);

public record AnalyticSystemLastUpdate([property: Key] string ZoneId, string? Date, [property: Column("NeedUpdate")] bool? NeedUpdate);

public record AnalyticSystemQuickLastUpdate([property: Key] string ZoneId, string? Date, [property: Column("NeedUpdate")] bool? NeedUpdate);

public record AnalyticSystemForceRefresh([property: Key] string ZoneId, bool? Result);

public record AnalyticSystemTemporaryTable([property: Key] string ZoneId, string? Context, string? Table);

public record AnalyticRawPasserby([property: Key] Guid Id, string ZoneId, string Uid, string UserMac, string DateStart, string DateEnd, string IsLocal);

public record AnalyticRawServiceVisitor([property: Key] Guid Id, string ZoneId, string Uid, string UserMac, string DateStart, string DateEnd, string IsLocal, string CntVisit, string Zones, string DeviceType, string Frequency, double MaxProximity);

public record AnalyticRawServiceVisitorLight([property: Key] Guid Id, string ZoneId, string Uid, string DateDay, string DateStart, string DateEnd);

public record AnalyticRawServiceVisitorMacList([property: Key] Guid Id, string ZoneId, string UserMac);

public record AnalyticRawServicePasserbyMacList([property: Key] Guid Id, string ZoneId, string UserMac);

public record AnalyticVisitCount([property: Key] string ZoneId, int Total);

public record AnalyticVisitCountDetails([property: Key] Guid Id, string ZoneId, string Date, int All);

public record AnalyticVisitCountHourDetails([property: Key] Guid Id, string ZoneId, string Hour, int All);

public record AnalyticVisitDuration([property: Key] Guid Id, string ZoneId, string Date, double Duration);

public record AnalyticVisitDurationDetails([property: Key] Guid Id, string ZoneId, string Date, double Duration0, double Duration300, double Duration900, double Duration1800);

public record AnalyticVisitCountHour([property: Key] Guid Id, string ZoneId, string Hour, double Count);

public record AnalyticVisitCountHourDay([property: Key] Guid Id, string ZoneId, string Day, string Hour, int Count);

public record AnalyticVisitCountHourDayStart([property: Key] Guid Id, string ZoneId, string Date, int Count);

public record Status([property: Key]string Name, double SyncProgressPercent);