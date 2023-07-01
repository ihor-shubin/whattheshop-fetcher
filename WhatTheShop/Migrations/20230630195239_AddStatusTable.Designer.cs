﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WhatTheShop.DB;

#nullable disable

namespace WhatTheShop.Migrations
{
    [DbContext(typeof(DbCtx))]
    [Migration("20230630195239_AddStatusTable")]
    partial class AddStatusTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("WhatTheShop.Models.AnalyticDeviceCount", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Android")
                        .HasColumnType("REAL");

                    b.Property<double?>("Ios")
                        .HasColumnType("REAL");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticDeviceCount");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticPasserbyBestTimes", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<string>("BestEndTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("BestStartTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PeakEndTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PeakStartTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticPasserbyBestTimes");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticPasserbyCount", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Frequent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Low")
                        .HasColumnType("INTEGER");

                    b.Property<int>("New")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticPasserbyCounts");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticPasserbyCountCommon", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Common")
                        .HasColumnType("REAL");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticPasserbyCountCommons");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticPasserbyCountDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticPasserbyCountDays");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticPasserbyCountDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Frequent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Low")
                        .HasColumnType("INTEGER");

                    b.Property<int>("New")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticPasserbyCountDetails");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticPasserbyCountHour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Count")
                        .HasColumnType("REAL");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticPasserbyCountHours");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticPasserbyCountSum", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Sum")
                        .HasColumnType("REAL");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticPasserbyCountSums");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticPassingCount", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticPassingCounts");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticPassingCountDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("All")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticPassingCountDetails");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticPassingCountHourDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("All")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticPassingCountHourDetails");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticRawPasserby", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DateEnd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DateStart")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IsLocal")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserMac")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticRawPasserby");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticRawServicePasserbyMacList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserMac")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticRawServicePasserbyMacList");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticRawServiceVisitor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CntVisit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DateEnd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DateStart")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Frequency")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IsLocal")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("MaxProximity")
                        .HasColumnType("REAL");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserMac")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Zones")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticRawServiceVisitor");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticRawServiceVisitorLight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DateDay")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DateEnd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DateStart")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticRawServiceVisitorLight");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticRawServiceVisitorMacList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserMac")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticRawServiceVisitorMacList");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticSensorCount", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<double?>("AbsoluteValue")
                        .HasColumnType("REAL");

                    b.Property<double?>("InValue")
                        .HasColumnType("REAL");

                    b.Property<double?>("OutValue")
                        .HasColumnType("REAL");

                    b.Property<string>("PresenceLastUpdate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("PresenceMaxValue")
                        .HasColumnType("REAL");

                    b.Property<double?>("PresenceValue")
                        .HasColumnType("REAL");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticSensorCount");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticSensorCountDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double?>("AbsoluteValue")
                        .HasColumnType("REAL");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("InValue")
                        .HasColumnType("REAL");

                    b.Property<double?>("OutValue")
                        .HasColumnType("REAL");

                    b.Property<double?>("PresenceValue")
                        .HasColumnType("REAL");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticSensorCountDetails");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticSystemForceRefresh", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("Result")
                        .HasColumnType("INTEGER");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticSystemForceRefresh");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticSystemLastUpdate", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("NeedUpdate")
                        .HasColumnType("INTEGER")
                        .HasColumnName("NeedUpdate");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticSystemLastUpdate");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticSystemQuickLastUpdate", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("NeedUpdate")
                        .HasColumnType("INTEGER")
                        .HasColumnName("NeedUpdate");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticSystemQuickLastUpdate");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticSystemTemporaryTable", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Context")
                        .HasColumnType("TEXT");

                    b.Property<string>("Table")
                        .HasColumnType("TEXT");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticSystemTemporaryTable");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitCount", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticVisitCount");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitCountDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("All")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitCountDetails");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitCountHour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Count")
                        .HasColumnType("REAL");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitCountHour");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitCountHourDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitCountHourDay");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitCountHourDayStart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitCountHourDayStart");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitCountHourDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("All")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitCountHourDetails");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitDuration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Duration")
                        .HasColumnType("REAL");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitDuration");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitDurationDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Duration0")
                        .HasColumnType("REAL");

                    b.Property<double>("Duration1800")
                        .HasColumnType("REAL");

                    b.Property<double>("Duration300")
                        .HasColumnType("REAL");

                    b.Property<double>("Duration900")
                        .HasColumnType("REAL");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitDurationDetails");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitorBestTimes", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<string>("BestEndTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("BestStartTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PeakEndTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PeakStartTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticVisitorBestTimes");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitorCount", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Frequent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Low")
                        .HasColumnType("INTEGER");

                    b.Property<int>("New")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticVisitorCounts");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitorCountCommon", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Count")
                        .HasColumnType("REAL");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticVisitorCountCommon");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitorCountDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("All")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Frequent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Low")
                        .HasColumnType("INTEGER");

                    b.Property<int>("New")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitorCountDetails");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitorCountHour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Count")
                        .HasColumnType("REAL");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitorCountHours");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitorCountHourDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitorCountHourDays");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitorCountHourDayDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Count")
                        .HasColumnType("REAL");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitorCountHourDayDetails");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitorCountHourDayStart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitorCountHourDayStart");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitorCountSum", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Count")
                        .HasColumnType("REAL");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitorCountSum");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitorDuration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Duration")
                        .HasColumnType("REAL");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitorDurations");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticVisitorDurationDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Duration0")
                        .HasColumnType("REAL");

                    b.Property<double>("Duration1800")
                        .HasColumnType("REAL");

                    b.Property<double>("Duration300")
                        .HasColumnType("REAL");

                    b.Property<double>("Duration900")
                        .HasColumnType("REAL");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticVisitorDurationDetails");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticZonesGeneral", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Average")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cnt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Max")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Min")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Percentage")
                        .HasColumnType("INTEGER");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticZonesGeneral");
                });

            modelBuilder.Entity("WhatTheShop.Models.AnalyticZonesVenn", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Count")
                        .HasColumnType("REAL");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticZonesVenn");
                });

            modelBuilder.Entity("WhatTheShop.Models.Device", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("CryptTokens")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxDuration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ts")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("WhatTheShop.Models.MediaVisitCount", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Facebook")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Google")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Teemo")
                        .HasColumnType("INTEGER");

                    b.HasKey("ZoneId");

                    b.ToTable("MediaVisitCount");
                });

            modelBuilder.Entity("WhatTheShop.Models.MediaVisitCountDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Facebook")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Google")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Teemo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MediaVisitCountDetails");
                });

            modelBuilder.Entity("WhatTheShop.Models.Monitoring", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<int>("DevicesOn")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DevicesTotal")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SensorsTotal")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZoneName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ZoneId");

                    b.ToTable("Monitorings");
                });

            modelBuilder.Entity("WhatTheShop.Models.Status", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("SyncProgressPercent")
                        .HasColumnType("REAL");

                    b.HasKey("Name");

                    b.ToTable("AAStatus");
                });

            modelBuilder.Entity("WhatTheShop.Models.Zone", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("WhatTheShop.Models.ZoneInfo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Children")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lat")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lon")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Sensors")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ZonesInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
