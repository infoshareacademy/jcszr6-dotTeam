using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Enums;

namespace PlanAndRide.Database
{
    public class PlanAndRideContext : DbContext
    {
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GeoCoordinate> GeoCoordinates { get; set; }
        

        

        public PlanAndRideContext()
        {

        }
        public PlanAndRideContext(DbContextOptions<PlanAndRideContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost;Database=PlanAndRideDb;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Ride>()
                .HasOne(r => r.User).WithMany(u => u.CreatedRides)
                .HasForeignKey(r => r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Ride>()
                .HasMany(r => r.RideMembers).WithMany(u => u.AttendedRides)
                .UsingEntity<UserRide>(
                    ur => ur.HasOne(ur => ur.User).WithMany(u => u.UserRide).HasForeignKey(ur => ur.UserId),
                    ur => ur.HasOne(ur => ur.Ride).WithMany(r => r.UserRide).HasForeignKey(ur => ur.RideId));

            modelBuilder.Entity<Route>()
                .HasOne(r => r.User)
                .WithMany(u => u.Routes)
                .HasForeignKey("UserId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User).WithMany(u => u.Reviews)
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
            modelBuilder.Entity<Ride>()
                .Property<int>("StatusRide").HasMaxLength(1);

            modelBuilder.Entity<Route>()
                .Property<string>("Description").HasMaxLength(255);
            modelBuilder.Entity<Route>()
                .Property<string>("DestinationCity").HasMaxLength(100);
            modelBuilder.Entity<Route>()
                .Property<string>("Name").HasMaxLength(60);
            modelBuilder.Entity<Route>()
                .Property<string>("StartingCity").HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property<string>("Email").HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property<string>("Login").HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property<string>("Password").HasMaxLength(60);
        }
    }
}
