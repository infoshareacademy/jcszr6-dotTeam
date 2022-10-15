using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Exceptions;


namespace PlanAndRide.Database.Repository
{
    public class RideRepository : IRideRepository
    {
        private readonly PlanAndRideContext _context;
        public RideRepository(PlanAndRideContext context)
        {
            _context = context;
        }
        public async Task<Ride> Get(int id)
        {
            try
            {
                return await _context.Rides
                    .Include(ride => ride.ApplicationUser)
                    .Include(ride => ride.Route.StartingPosition)
                    .Include(ride => ride.Route.DestinationPosition)
                    .Include(ride => ride.RideMembers)
                    .SingleOrDefaultAsync(ride => ride.Id == id);   
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Ride>> GetAll()
        {
            return await _context.Rides
                .Include(r => r.Route)
                .ToListAsync();
        }
        public async Task<IEnumerable<Ride>> GetByUser(string id)
        {
            return await _context.Rides
                .Where(r => r.ApplicationUser.Id == id)
                .Include(r => r.Route)
                .ToListAsync();
        }
        public async Task<IEnumerable<Ride>> GetPublic()
        {
            return await _context.Rides
                .Where(r => r.IsPrivate == false)
                .Include(r => r.Route)
                .Include(r => r.ApplicationUser)
                .ToListAsync();
        }
        public async Task Add(Ride ride)
        {
            if(ride.Route != null)
            {
                var existingRoute = await _context.Routes.FindAsync(ride.Route.Id);
                ride.Route = existingRoute;
            }
            await _context.Rides.AddAsync(ride);
            _context.SaveChanges();
        }
        public async Task Update(int id, Ride ride)
        {
            Ride existingRide;
            try
            {
                existingRide = await _context.Rides
                    .Include(r=>r.Route)
                    .SingleOrDefaultAsync(r => r.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Unique key violation: Ride ID:{id}");
            }

            if (existingRide == null)
            {
                throw new RecordNotFoundException($"Ride ID:{id} not found in repository");
            }
            if(ride.Route == null)
            {
                existingRide.Route = null;
            }
            else
            {
                existingRide.Route = await _context.Routes.FindAsync(ride.Route.Id);
            }
            existingRide.Name = ride.Name;
            existingRide.Date = ride.Date;
            existingRide.IsPrivate = ride.IsPrivate;
            existingRide.ShareRide = ride.ShareRide;
            existingRide.Description = ride.Description;
            existingRide.RideMembers = ride.RideMembers;

            _context.SaveChanges();
        }
        public async Task Delete(int id)
        {
            try
            {
                var ride = await _context.Rides.SingleOrDefaultAsync(u => u.Id == id);
                _context.Rides.Remove(ride);
                _context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Unique key violation: Ride ID:{id}");
            }
        }
        public async Task AddRideMember(Ride ride, string userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return;
            ride.RideMembers.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveRideMember(Ride ride, string userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return;
            ride.RideMembers.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
