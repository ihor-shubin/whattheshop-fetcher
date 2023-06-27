using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatTheShop.Migrations
{
    /// <inheritdoc />
    public partial class AddTables3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyticVisitorDurationDetails");
        }
    }
}
