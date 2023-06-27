using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatTheShop.Migrations
{
    /// <inheritdoc />
    public partial class AddTables5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Count",
                table: "AnalyticPasserbyCountHours",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyticVisitorCountHours");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "AnalyticPasserbyCountHours",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
