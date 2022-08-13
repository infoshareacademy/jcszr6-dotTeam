using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Database
{
    public class PlanAndRideContext:DbContext
    {
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GeoCoordinate> GeoCoordinates { get; set; }
        public PlanAndRideContext()
        {

        }
        public PlanAndRideContext(DbContextOptions<PlanAndRideContext> options) :base(options)
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

            //modelBuilder.Entity<GeoCoordinate>()
            //    .HasKey(g => new { g.Latitude, g.Longitude });
            //modelBuilder.Entity<GeoCoordinate>()
            //    .Property(g => g.Altitude).HasDefaultValue(0);
            //modelBuilder.Entity<GeoCoordinate>()
            //    .Property(g => g.Course).HasDefaultValue(0);
            //modelBuilder.Entity<GeoCoordinate>()
            //    .Property(g => g.HorizontalAccuracy).HasDefaultValue(0);
            //modelBuilder.Entity<GeoCoordinate>()
            //    .Property(g => g.Speed).HasDefaultValue(0);
            //modelBuilder.Entity<GeoCoordinate>()
            //    .Property(g => g.VerticalAccuracy).HasDefaultValue(0);
            modelBuilder.Entity<GeoCoordinate>(b =>
            {
                b.HasKey(e => new { e.Latitude, e.Longitude });
                b.Property(g => g.Altitude).HasDefaultValue(0);
                b.Property(g => g.Course).HasDefaultValue(0);
                b.Property(g=>g.HorizontalAccuracy).HasDefaultValue(0);
                b.Property(g => g.Speed).HasDefaultValue(0);
                b.Property(g => g.VerticalAccuracy).HasDefaultValue(0);
            });

            modelBuilder.Entity<Ride>()
                .HasOne(r=>r.User).WithMany(u=>u.CreatedRides)
                .HasForeignKey(r=>r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ride>()
                .HasMany(r => r.RideMembers).WithMany(u => u.AttendedRides)
                .UsingEntity<UserRide>(
                    ur=>ur.HasOne(ur=>ur.User).WithMany(u=>u.UserRide).HasForeignKey(ur=>ur.UserId),
                    ur=>ur.HasOne(ur=>ur.Ride).WithMany(r=>r.UserRide).HasForeignKey(ur=>ur.RideId)
                );
                
            modelBuilder.Entity<Review>()
                .HasOne(r=>r.User).WithMany(u=>u.Reviews)
                .HasForeignKey(r=>r.UserId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
                .HasOne(rw => rw.Route).WithMany(r => r.Reviews)
                .HasForeignKey(rw => rw.RouteId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>(b =>
            {
                b.Property<string>("Description").HasMaxLength(255);
            });

            modelBuilder.Entity<Ride>(b =>
            {
                b.Property<string>("Description").HasMaxLength(255);
                b.Property<string>("Name").HasMaxLength(60);
            });

            modelBuilder.Entity<Route>(b =>
            {
                b.Property<string>("Description").HasMaxLength(255);
                b.Property<string>("DestinationCity").HasMaxLength(255);
                b.Property<string>("Name").HasMaxLength(60);
                b.Property<string>("StartingCity").HasMaxLength(255);
            });

            modelBuilder.Entity<User>(b =>
            {
                b.Property<string>("Email").HasMaxLength(255);
                b.Property<string>("Login").HasMaxLength(255);
                b.Property<string>("Password").HasMaxLength(255);
            });
        }
    }
}
