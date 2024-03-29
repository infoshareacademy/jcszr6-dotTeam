﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlanAndRide.Database;

#nullable disable

namespace PlanAndRide.Database.Migrations
{
    [DbContext(typeof(PlanAndRideContext))]
    [Migration("20220913085749_AddEncodedPathAndWaypointsToRoute")]
    partial class AddEncodedPathAndWaypointsToRoute
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PlanAndRide.BusinessLogic.GeoCoordinate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("GeoCoordinates");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.Ride", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int?>("RouteId")
                        .HasColumnType("int");

                    b.Property<bool>("ShareRide")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("UserId");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("DestinationCity")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("DestinationPositionId")
                        .HasColumnType("int");

                    b.Property<string>("EncodedGoogleMapsPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EncodedGoogleMapsWaypoints")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<bool>("ShareRoute")
                        .HasColumnType("bit");

                    b.Property<string>("StartingCity")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("StartingPositionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DestinationPositionId");

                    b.HasIndex("StartingPositionId");

                    b.HasIndex("UserId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.UserRide", b =>
                {
                    b.Property<int>("RideId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RideId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRide");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.Review", b =>
                {
                    b.HasOne("PlanAndRide.BusinessLogic.Route", "Route")
                        .WithMany("Reviews")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlanAndRide.BusinessLogic.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.Ride", b =>
                {
                    b.HasOne("PlanAndRide.BusinessLogic.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId");

                    b.HasOne("PlanAndRide.BusinessLogic.User", "User")
                        .WithMany("CreatedRides")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.Route", b =>
                {
                    b.HasOne("PlanAndRide.BusinessLogic.GeoCoordinate", "DestinationPosition")
                        .WithMany()
                        .HasForeignKey("DestinationPositionId");

                    b.HasOne("PlanAndRide.BusinessLogic.GeoCoordinate", "StartingPosition")
                        .WithMany()
                        .HasForeignKey("StartingPositionId");

                    b.HasOne("PlanAndRide.BusinessLogic.User", "User")
                        .WithMany("Routes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DestinationPosition");

                    b.Navigation("StartingPosition");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.UserRide", b =>
                {
                    b.HasOne("PlanAndRide.BusinessLogic.Ride", "Ride")
                        .WithMany("UserRide")
                        .HasForeignKey("RideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlanAndRide.BusinessLogic.User", "User")
                        .WithMany("UserRide")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ride");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.Ride", b =>
                {
                    b.Navigation("UserRide");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.Route", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PlanAndRide.BusinessLogic.User", b =>
                {
                    b.Navigation("CreatedRides");

                    b.Navigation("Reviews");

                    b.Navigation("Routes");

                    b.Navigation("UserRide");
                });
#pragma warning restore 612, 618
        }
    }
}
