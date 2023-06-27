using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatTheShop.Migrations
{
    /// <inheritdoc />
    public partial class AddTables4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyticVisitorBestTimes");
        }
    }
}
