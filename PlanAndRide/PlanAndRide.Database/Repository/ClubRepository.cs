using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Exceptions;

namespace PlanAndRide.Database.Repository
{
    public class ClubRepository : IRepository<Club>
    {
        private readonly PlanAndRideContext _context;

        public ClubRepository(PlanAndRideContext context)
        {
            _context = context;
        }
        public async Task<Club> Get(int id)
        {
            try
            {
                return await _context.Clubs.SingleOrDefaultAsync(r => r.Id == id);
            }
            catch
            {
                throw new InvalidOperationException($"Unique key violaton: Club ID:{id}");
            }
        }
        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }

        public async Task Add(Club club)
        {
            //var userId = 1;
            var userId = Guid.NewGuid().ToString();
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException("User not found at club create");

            club.ApplicationUser = user;

            await _context.Clubs.AddAsync(club);
            _context.SaveChanges();
        }
        public async Task Update(int id, Club club)
        {
            Club existingClub;
            try
            {
                existingClub = await _context.Clubs.SingleOrDefaultAsync(r => r.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Unique key violaton: Club ID:{id}");
            }

            if (existingClub == null)
            {
                throw new RecordNotFoundException($"Club ID:{id} not found in repository");
            }
            existingClub.Name = club.Name;
            existingClub.Description = club.Description;
            existingClub.ShareRide = club.ShareRide;
            existingClub.IsPrivate = club.IsPrivate;
            existingClub.ClubMembers = club.ClubMembers;

            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            try
            {
                var club = await _context.Clubs.SingleOrDefaultAsync(u => u.Id == id);
                _context.Clubs.Remove(club);
                _context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Unique key violaton: Club ID:{id}");
            }
        }

        public Task<IEnumerable<Club>> GetByUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}