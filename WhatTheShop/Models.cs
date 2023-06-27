
using System.ComponentModel.DataAnnotations;

namespace WhatTheShop;

public record Zone([property:Key]string Id, string Type, string Name, string Fullname);

public record ZoneInfo([property:Key]string Id, string Type, string Name, string Fullname, string Address, string ZipCode, string City, string Country, string Lat, string Lon, string Children, string Sensors);

public record Device([property:Key]string Name, int Ts, int MaxDuration, string CryptTokens);

public record Monitoring([property: Key] string ZoneId, string ZoneName, int DevicesOn, int DevicesTotal, int SensorsTotal);

public record AnalyticPasserbyCount([property: Key] string ZoneId, int New, int Low, int Frequent, int Total);

public record AnalyticPasserbyCountDetails([property: Key]Guid Id, string ZoneId, string Date, int New, int Low, int Frequent, int Total);

public record AnalyticPasserbyCountHour([property: Key]Guid Id, string ZoneId, string Hour, double Count);

public record AnalyticPasserbyCountDay([property: Key]Guid Id, string ZoneId, string Day, string Hour, int Count);

public record AnalyticPasserbyCountSum([property: Key]Guid Id, string ZoneId, string Day, double Sum);

public record AnalyticPasserbyBestTimes([property: Key]string ZoneId, string BestStartTime, string BestEndTime, string PeakStartTime, string PeakEndTime);

public record AnalyticPasserbyCountCommon([property: Key]string ZoneId, double Common);

public record AnalyticPassingCount([property: Key]string ZoneId, int Total);

public record AnalyticPassingCountDetails([property: Key]Guid Id, string ZoneId, string Date, int All);

public record AnalyticPassingCountHourDetails([property: Key]Guid Id, string ZoneId, string Hour, int All);

public record AnalyticVisitorCount([property: Key] string ZoneId, int New, int Low, int Frequent, int Total);

public record AnalyticVisitorCountDetails([property: Key]Guid Id, string ZoneId, string Date, int New, int Low, int Frequent, int All);

public record AnalyticVisitorDuration([property: Key]Guid Id, string ZoneId, string Date, double Duration);

public record AnalyticVisitorDurationDetails([property: Key]Guid Id, string ZoneId, string Date, double Duration0, double Duration300, double Duration900, double Duration1800);

public record AnalyticVisitorBestTimes([property: Key]string ZoneId, string BestStartTime, string BestEndTime, string PeakStartTime, string PeakEndTime);

public record AnalyticVisitorCountHour([property: Key]Guid Id, string ZoneId, string Hour, double Count);

public record CampaignList(string ZoneId, string ZoneName, List<string> Campaigns);

public record CampaignCount(string ZoneId, string ZoneName, string CampaignId, int Total, int Confirmed, int Target, int TargetDuration);
