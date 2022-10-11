using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Enums;
using PlanAndRide.BusinessLogic.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PlanAndRide.Database.Repository
{
    public class RideRepository : IRepository<Ride>
    {
        private readonly PlanAndRideContext _context;
        private readonly IMapper _mapper;
        public RideRepository(PlanAndRideContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                throw new InvalidOperationException($"Unique key violation: Ride ID:{id}");
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
                throw new InvalidOperationException($"Unique key violation: Ride ID:{id}");
            }
        }




    }
}
