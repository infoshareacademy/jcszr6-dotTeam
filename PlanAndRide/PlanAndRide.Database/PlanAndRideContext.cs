﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;


namespace PlanAndRide.Database
{
    public class PlanAndRideContext : IdentityDbContext
    {
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<GeoCoordinate> GeoCoordinates { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public PlanAndRideContext()
        {

        }
        public PlanAndRideContext(DbContextOptions<PlanAndRideContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost;Database=PlanAndRideDataBase;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Ride>()
                .HasOne(r => r.ApplicationUser).WithMany(u => u.CreatedRides)
                .HasForeignKey(r => r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Ride>()
                .HasMany(r => r.RideMembers).WithMany(u => u.AttendedRides)
                .UsingEntity<UserRide>(
                    ur => ur.HasOne(ur => ur.ApplicationUser).WithMany(u => u.UserRide).HasForeignKey(ur => ur.UserId),
                    ur => ur.HasOne(ur => ur.Ride).WithMany(r => r.UserRide).HasForeignKey(ur => ur.RideId));

            modelBuilder.Entity<Club>()
                .HasOne(c => c.ApplicationUser).WithMany(u => u.CreatedClubs)
                .HasForeignKey(r => r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserClub>()
                .HasOne(uc => uc.ApplicationUser)
                .WithMany(u => u.UserClubs)
                .HasForeignKey("UserId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserClub>().HasKey("UserId");

            modelBuilder.Entity<Route>()
                .HasOne(r => r.ApplicationUser)
                .WithMany(u => u.Routes)
                .HasForeignKey("UserId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.ApplicationUser).WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(rw => rw.Route).WithMany(r => r.Reviews)
                .HasForeignKey(rw => rw.RouteId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .Property<string>("Description").HasMaxLength(255);


            modelBuilder.Entity<Ride>()
                .Property<string>("Description").HasMaxLength(255);
            modelBuilder.Entity<Ride>()
                .Property<string>("Name").HasMaxLength(60);


            modelBuilder.Entity<Route>()
                .Property<string>("Description").HasMaxLength(255);
            modelBuilder.Entity<Route>()
                .Property<string>("DestinationCity").HasMaxLength(100);
            modelBuilder.Entity<Route>()
                .Property<string>("Name").HasMaxLength(60);
            modelBuilder.Entity<Route>()
                .Property<string>("StartingCity").HasMaxLength(100);

            modelBuilder.Entity<ApplicationUser>()
                .Property<string>("Email").HasMaxLength(100);
            modelBuilder.Entity<ApplicationUser>()
                .Property<string>("Login").HasMaxLength(100);
            modelBuilder.Entity<ApplicationUser>()
                .Property<string>("Password").HasMaxLength(60);

            modelBuilder.Entity<Club>()
                .Property<string>("Description").HasMaxLength(255);
            modelBuilder.Entity<Club>()
                .Property<string>("Name").HasMaxLength(60);
        }
    }
}
