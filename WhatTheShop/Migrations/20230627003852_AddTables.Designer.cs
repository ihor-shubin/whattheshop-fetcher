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
    [Migration("20230627003852_AddTables")]
    partial class AddTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("WhatTheShop.AnalyticPasserbyBestTimes", b =>
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

            modelBuilder.Entity("WhatTheShop.AnalyticPasserbyCount", b =>
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

            modelBuilder.Entity("WhatTheShop.AnalyticPasserbyCountCommon", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Common")
                        .HasColumnType("REAL");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticPasserbyCountCommons");
                });

            modelBuilder.Entity("WhatTheShop.AnalyticPasserbyCountDay", b =>
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

            modelBuilder.Entity("WhatTheShop.AnalyticPasserbyCountDetails", b =>
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

            modelBuilder.Entity("WhatTheShop.AnalyticPasserbyCountHour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZoneId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnalyticPasserbyCountHours");
                });

            modelBuilder.Entity("WhatTheShop.AnalyticPasserbyCountSum", b =>
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

            modelBuilder.Entity("WhatTheShop.AnalyticPassingCount", b =>
                {
                    b.Property<string>("ZoneId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.HasKey("ZoneId");

                    b.ToTable("AnalyticPassingCounts");
                });

            modelBuilder.Entity("WhatTheShop.AnalyticPassingCountDetails", b =>
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

            modelBuilder.Entity("WhatTheShop.AnalyticPassingCountHourDetails", b =>
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

            modelBuilder.Entity("WhatTheShop.AnalyticVisitorCount", b =>
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

            modelBuilder.Entity("WhatTheShop.AnalyticVisitorCountDetails", b =>
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

            modelBuilder.Entity("WhatTheShop.Device", b =>
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

            modelBuilder.Entity("WhatTheShop.Monitoring", b =>
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

            modelBuilder.Entity("WhatTheShop.Zone", b =>
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

            modelBuilder.Entity("WhatTheShop.ZoneInfo", b =>
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
