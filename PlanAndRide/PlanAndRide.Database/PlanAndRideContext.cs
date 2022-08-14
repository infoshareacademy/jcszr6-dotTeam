using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Database
{
    public class PlanAndRideContext:DbContext
    {
        private readonly bool _useLazyLoading;

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
        public PlanAndRideContext(bool useLazyLoading)
        {
            _useLazyLoading = useLazyLoading;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (_useLazyLoading)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
            optionsBuilder.UseSqlServer("Server=localhost;Database=PlanAndRideDb;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


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
            modelBuilder.Entity<Route>()
                .HasOne(r => r.User)
                .WithMany(u => u.Routes)
                .HasForeignKey("UserId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
                
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
