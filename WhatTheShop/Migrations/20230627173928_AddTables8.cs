using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatTheShop.Migrations
{
    /// <inheritdoc />
    public partial class AddTables8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyticVisitorCountHourDayStart");

            migrationBuilder.DropTable(
                name: "AnalyticVisitorCountSum");
        }
    }
}
