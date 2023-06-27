using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatTheShop.Migrations
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "AnalyticVisitorCountDetails");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorCounts");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Monitorings");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "ZonesInfos");
        }
    }
}
