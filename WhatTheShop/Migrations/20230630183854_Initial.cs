using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatTheShop.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalyticDeviceCount",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Android = table.Column<double>(type: "REAL", nullable: true),
                    Ios = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticDeviceCount", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticPasserbyBestTimes",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    BestStartTime = table.Column<string>(type: "TEXT", nullable: false),
                    BestEndTime = table.Column<string>(type: "TEXT", nullable: false),
                    PeakStartTime = table.Column<string>(type: "TEXT", nullable: false),
                    PeakEndTime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticPasserbyBestTimes", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticPasserbyCountCommons",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Common = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticPasserbyCountCommons", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticPasserbyCountDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Day = table.Column<string>(type: "TEXT", nullable: false),
                    Hour = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticPasserbyCountDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticPasserbyCountDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    New = table.Column<int>(type: "INTEGER", nullable: false),
                    Low = table.Column<int>(type: "INTEGER", nullable: false),
                    Frequent = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticPasserbyCountDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticPasserbyCountHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Hour = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticPasserbyCountHours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticPasserbyCounts",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    New = table.Column<int>(type: "INTEGER", nullable: false),
                    Low = table.Column<int>(type: "INTEGER", nullable: false),
                    Frequent = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticPasserbyCounts", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticPasserbyCountSums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Day = table.Column<string>(type: "TEXT", nullable: false),
                    Sum = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticPasserbyCountSums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticPassingCountDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    All = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticPassingCountDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticPassingCountHourDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Hour = table.Column<string>(type: "TEXT", nullable: false),
                    All = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticPassingCountHourDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticPassingCounts",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticPassingCounts", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticRawPasserby",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Uid = table.Column<string>(type: "TEXT", nullable: false),
                    UserMac = table.Column<string>(type: "TEXT", nullable: false),
                    DateStart = table.Column<string>(type: "TEXT", nullable: false),
                    DateEnd = table.Column<string>(type: "TEXT", nullable: false),
                    IsLocal = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticRawPasserby", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticRawServicePasserbyMacList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    UserMac = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticRawServicePasserbyMacList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticRawServiceVisitor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Uid = table.Column<string>(type: "TEXT", nullable: false),
                    UserMac = table.Column<string>(type: "TEXT", nullable: false),
                    DateStart = table.Column<string>(type: "TEXT", nullable: false),
                    DateEnd = table.Column<string>(type: "TEXT", nullable: false),
                    IsLocal = table.Column<string>(type: "TEXT", nullable: false),
                    CntVisit = table.Column<string>(type: "TEXT", nullable: false),
                    Zones = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceType = table.Column<string>(type: "TEXT", nullable: false),
                    Frequency = table.Column<string>(type: "TEXT", nullable: false),
                    MaxProximity = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticRawServiceVisitor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticRawServiceVisitorLight",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Uid = table.Column<string>(type: "TEXT", nullable: false),
                    DateDay = table.Column<string>(type: "TEXT", nullable: false),
                    DateStart = table.Column<string>(type: "TEXT", nullable: false),
                    DateEnd = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticRawServiceVisitorLight", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticRawServiceVisitorMacList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    UserMac = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticRawServiceVisitorMacList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticSensorCount",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    PresenceValue = table.Column<double>(type: "REAL", nullable: true),
                    PresenceMaxValue = table.Column<double>(type: "REAL", nullable: true),
                    PresenceLastUpdate = table.Column<string>(type: "TEXT", nullable: true),
                    InValue = table.Column<double>(type: "REAL", nullable: true),
                    OutValue = table.Column<double>(type: "REAL", nullable: true),
                    AbsoluteValue = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticSensorCount", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticSensorCountDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    PresenceValue = table.Column<double>(type: "REAL", nullable: true),
                    InValue = table.Column<double>(type: "REAL", nullable: true),
                    OutValue = table.Column<double>(type: "REAL", nullable: true),
                    AbsoluteValue = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticSensorCountDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticSystemForceRefresh",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Result = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticSystemForceRefresh", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticSystemLastUpdate",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: true),
                    NeedUpdate = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticSystemLastUpdate", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticSystemQuickLastUpdate",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: true),
                    NeedUpdate = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticSystemQuickLastUpdate", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticSystemTemporaryTable",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Context = table.Column<string>(type: "TEXT", nullable: true),
                    Table = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticSystemTemporaryTable", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitCount",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitCount", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitCountDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    All = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitCountDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitCountHour",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Hour = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitCountHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitCountHourDay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Day = table.Column<string>(type: "TEXT", nullable: false),
                    Hour = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitCountHourDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitCountHourDayStart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitCountHourDayStart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitCountHourDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Hour = table.Column<string>(type: "TEXT", nullable: false),
                    All = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitCountHourDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitDuration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Duration = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitDuration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitDurationDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Duration0 = table.Column<double>(type: "REAL", nullable: false),
                    Duration300 = table.Column<double>(type: "REAL", nullable: false),
                    Duration900 = table.Column<double>(type: "REAL", nullable: false),
                    Duration1800 = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitDurationDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitorBestTimes",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    BestStartTime = table.Column<string>(type: "TEXT", nullable: false),
                    BestEndTime = table.Column<string>(type: "TEXT", nullable: false),
                    PeakStartTime = table.Column<string>(type: "TEXT", nullable: false),
                    PeakEndTime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitorBestTimes", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitorCountCommon",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitorCountCommon", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitorCountDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    New = table.Column<int>(type: "INTEGER", nullable: false),
                    Low = table.Column<int>(type: "INTEGER", nullable: false),
                    Frequent = table.Column<int>(type: "INTEGER", nullable: false),
                    All = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitorCountDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitorCountHourDayDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitorCountHourDayDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitorCountHourDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Day = table.Column<string>(type: "TEXT", nullable: false),
                    Hour = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitorCountHourDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitorCountHourDayStart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitorCountHourDayStart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitorCountHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Hour = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitorCountHours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitorCounts",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    New = table.Column<int>(type: "INTEGER", nullable: false),
                    Low = table.Column<int>(type: "INTEGER", nullable: false),
                    Frequent = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitorCounts", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitorCountSum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Hour = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitorCountSum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitorDurationDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Duration0 = table.Column<double>(type: "REAL", nullable: false),
                    Duration300 = table.Column<double>(type: "REAL", nullable: false),
                    Duration900 = table.Column<double>(type: "REAL", nullable: false),
                    Duration1800 = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitorDurationDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticVisitorDurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Duration = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticVisitorDurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticZonesGeneral",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Percentage = table.Column<int>(type: "INTEGER", nullable: false),
                    Cnt = table.Column<string>(type: "TEXT", nullable: true),
                    Average = table.Column<int>(type: "INTEGER", nullable: false),
                    Min = table.Column<string>(type: "TEXT", nullable: false),
                    Max = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticZonesGeneral", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticZonesVenn",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Count = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticZonesVenn", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Ts = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    CryptTokens = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "MediaVisitCount",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Facebook = table.Column<int>(type: "INTEGER", nullable: false),
                    Google = table.Column<int>(type: "INTEGER", nullable: false),
                    Teemo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaVisitCount", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "MediaVisitCountDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Facebook = table.Column<int>(type: "INTEGER", nullable: false),
                    Google = table.Column<int>(type: "INTEGER", nullable: false),
                    Teemo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaVisitCountDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monitorings",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "TEXT", nullable: false),
                    ZoneName = table.Column<string>(type: "TEXT", nullable: false),
                    DevicesOn = table.Column<int>(type: "INTEGER", nullable: false),
                    DevicesTotal = table.Column<int>(type: "INTEGER", nullable: false),
                    SensorsTotal = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitorings", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Fullname = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZonesInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Fullname = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Lat = table.Column<string>(type: "TEXT", nullable: false),
                    Lon = table.Column<string>(type: "TEXT", nullable: false),
                    Children = table.Column<string>(type: "TEXT", nullable: false),
                    Sensors = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZonesInfos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyticDeviceCount");

            migrationBuilder.DropTable(
                name: "AnalyticPasserbyBestTimes");

            migrationBuilder.DropTable(
                name: "AnalyticPasserbyCountCommons");

            migrationBuilder.DropTable(
                name: "AnalyticPasserbyCountDays");

            migrationBuilder.DropTable(
                name: "AnalyticPasserbyCountDetails");

            migrationBuilder.DropTable(
                name: "AnalyticPasserbyCountHours");

            migrationBuilder.DropTable(
                name: "AnalyticPasserbyCounts");

            migrationBuilder.DropTable(
                name: "AnalyticPasserbyCountSums");

            migrationBuilder.DropTable(
                name: "AnalyticPassingCountDetails");

            migrationBuilder.DropTable(
                name: "AnalyticPassingCountHourDetails");

            migrationBuilder.DropTable(
                name: "AnalyticPassingCounts");

            migrationBuilder.DropTable(
                name: "AnalyticRawPasserby");

            migrationBuilder.DropTable(
                name: "AnalyticRawServicePasserbyMacList");

            migrationBuilder.DropTable(
                name: "AnalyticRawServiceVisitor");

            migrationBuilder.DropTable(
                name: "AnalyticRawServiceVisitorLight");

            migrationBuilder.DropTable(
                name: "AnalyticRawServiceVisitorMacList");

            migrationBuilder.DropTable(
                name: "AnalyticSensorCount");

            migrationBuilder.DropTable(
                name: "AnalyticSensorCountDetails");

            migrationBuilder.DropTable(
                name: "AnalyticSystemForceRefresh");

            migrationBuilder.DropTable(
                name: "AnalyticSystemLastUpdate");

            migrationBuilder.DropTable(
                name: "AnalyticSystemQuickLastUpdate");

            migrationBuilder.DropTable(
                name: "AnalyticSystemTemporaryTable");

            migrationBuilder.DropTable(
                name: "AnalyticVisitCount");

            migrationBuilder.DropTable(
                name: "AnalyticVisitCountDetails");

            migrationBuilder.DropTable(
                name: "AnalyticVisitCountHour");

            migrationBuilder.DropTable(
                name: "AnalyticVisitCountHourDay");

            migrationBuilder.DropTable(
                name: "AnalyticVisitCountHourDayStart");

            migrationBuilder.DropTable(
                name: "AnalyticVisitCountHourDetails");

            migrationBuilder.DropTable(
                name: "AnalyticVisitDuration");

            migrationBuilder.DropTable(
                name: "AnalyticVisitDurationDetails");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorBestTimes");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorCountCommon");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorCountDetails");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorCountHourDayDetails");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorCountHourDays");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorCountHourDayStart");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorCountHours");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorCounts");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorCountSum");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorDurationDetails");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorDurations");

            migrationBuilder.DropTable(
                name: "AnalyticZonesGeneral");

            migrationBuilder.DropTable(
                name: "AnalyticZonesVenn");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "MediaVisitCount");

            migrationBuilder.DropTable(
                name: "MediaVisitCountDetails");

            migrationBuilder.DropTable(
                name: "Monitorings");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "ZonesInfos");
        }
    }
}
