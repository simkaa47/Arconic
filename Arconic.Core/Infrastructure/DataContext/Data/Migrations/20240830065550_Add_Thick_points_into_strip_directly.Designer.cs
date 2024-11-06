﻿// <auto-generated />
using System;
using Arconic.Core.Infrastructure.DataContext.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Arconic.Core.Infrastructure.DataContext.Data.Migrations
{
    [DbContext(typeof(ArconicDbContext))]
    [Migration("20240830065550_Add_Thick_points_into_strip_directly")]
    partial class Add_Thick_points_into_strip_directly
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("Arconic.Core.Models.AccessControl.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Arconic.Core.Models.Event.EventHistoryItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("EventCode")
                        .HasMaxLength(4)
                        .HasColumnType("TEXT");

                    b.Property<int>("EventType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<long?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("EventHistoryItems");
                });

            modelBuilder.Entity("Arconic.Core.Models.Trends.Scan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Chechewitsa")
                        .HasColumnType("REAL");

                    b.Property<float>("Klin")
                        .HasColumnType("REAL");

                    b.Property<long>("StripId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Width")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("StripId");

                    b.ToTable("Scans");
                });

            modelBuilder.Entity("Arconic.Core.Models.Trends.Strip", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<float>("ExpectedThick")
                        .HasColumnType("REAL");

                    b.Property<float>("ExpectedWidth")
                        .HasColumnType("REAL");

                    b.Property<float>("MaxExpectedThick")
                        .HasColumnType("REAL");

                    b.Property<float>("MaxExpectedWidth")
                        .HasColumnType("REAL");

                    b.Property<int>("MeasMode")
                        .HasColumnType("INTEGER");

                    b.Property<float>("MinExpectedThick")
                        .HasColumnType("REAL");

                    b.Property<float>("MinExpectedWidth")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("SteelLabel")
                        .IsRequired()
                        .HasMaxLength(26)
                        .HasColumnType("TEXT");

                    b.Property<string>("StripId")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Strips");
                });

            modelBuilder.Entity("Arconic.Core.Models.Trends.ThickPoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<float>("Lendth")
                        .HasColumnType("REAL");

                    b.Property<float>("Position")
                        .HasColumnType("REAL");

                    b.Property<long>("ScanId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Speed")
                        .HasColumnType("REAL");

                    b.Property<long?>("StripId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Thick")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("ScanId");

                    b.HasIndex("StripId");

                    b.ToTable("ThickPoints");
                });

            modelBuilder.Entity("Arconic.Core.Models.Event.EventHistoryItem", b =>
                {
                    b.HasOne("Arconic.Core.Models.AccessControl.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Arconic.Core.Models.Trends.Scan", b =>
                {
                    b.HasOne("Arconic.Core.Models.Trends.Strip", "Strip")
                        .WithMany("Scans")
                        .HasForeignKey("StripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Strip");
                });

            modelBuilder.Entity("Arconic.Core.Models.Trends.ThickPoint", b =>
                {
                    b.HasOne("Arconic.Core.Models.Trends.Scan", "Scan")
                        .WithMany("ThickPoints")
                        .HasForeignKey("ScanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Arconic.Core.Models.Trends.Strip", null)
                        .WithMany("ThickPoints")
                        .HasForeignKey("StripId");

                    b.Navigation("Scan");
                });

            modelBuilder.Entity("Arconic.Core.Models.Trends.Scan", b =>
                {
                    b.Navigation("ThickPoints");
                });

            modelBuilder.Entity("Arconic.Core.Models.Trends.Strip", b =>
                {
                    b.Navigation("Scans");

                    b.Navigation("ThickPoints");
                });
#pragma warning restore 612, 618
        }
    }
}
