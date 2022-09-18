using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Enums;
using PlanAndRide.BusinessLogic.Exceptions;


namespace PlanAndRide.Database.Repository
{
    public class RideRepository : IRepository<Ride>
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
                return await _context.Rides.SingleOrDefaultAsync(r => r.Id == id);
            }
            catch
            {
                throw new InvalidOperationException($"Unique key violaton: Ride Id:{id}");
            }
        }
        public async Task<IEnumerable<Ride>> GetAll()
        {
            return await _context.Rides.ToListAsync();
        }
        public async Task Add(Ride ride)
        {
            var userId = 1;
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException("User not found at event create");
            ride.User = user;
            await _context.Rides.AddAsync(ride);
            _context.SaveChanges();
        }
        public async Task Update(int id, Ride ride)
        {
            Ride existingRide;
            try
            {
                existingRide = await _context.Rides.SingleOrDefaultAsync(r => r.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Unique key violaton: Ride ID:{id}");
            }

            if (existingRide == null)
            {
                throw new RecordNotFoundException($"Ride ID:{id} not found in repository");
            }
            existingRide.Name = ride.Name;
            existingRide.Date = ride.Date;
            existingRide.IsPrivate = ride.IsPrivate;
            existingRide.ShareRide = ride.ShareRide;
            existingRide.Route = ride.Route;
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
                throw new InvalidOperationException($"Unique key violaton: Ride ID:{id}");
            }
        }
        public async Task TimeStatusRide( Ride ride)
        {
            
            var compareDate = DateTime.Now.CompareTo(ride.Date, StatusList statuslist);
            if (compareDate > 0)
            {
                return ride.StatusRide = StatusLitst().
            }
            if (compareDate == 0)
            {
                ride.StatusRide = rightNowEvent;

            }

            else
            {
                ride.StatusRide = completed;
            }
        }
    } }
