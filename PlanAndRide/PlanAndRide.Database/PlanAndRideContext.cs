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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost;Database=PlanAndRideDb;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GeoCoordinate>()
                .HasKey(e => new { e.Latitude, e.Longitude });
                }
    }
}
