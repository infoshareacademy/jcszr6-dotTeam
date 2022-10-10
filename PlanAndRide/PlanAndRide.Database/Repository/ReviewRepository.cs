using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Exceptions;

namespace PlanAndRide.Database.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly PlanAndRideContext _context;

        public ReviewRepository(PlanAndRideContext context)
        {
            _context = context;
        }

        public async Task Add(Review entity)
        {
            await _context.Reviews.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                var review = await Get(id);
                if(review is null)
                {
                    throw new RecordNotFoundException($"Review id {id} not found in repository");
                }
                _context.Remove(review);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Review?> Get(int id)
        {
            try
            {
                return await _context.Reviews
                    .Include(r => r.ApplicationUser)
                    .Include(r => r.Route)
                    .SingleOrDefaultAsync(r => r.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews
                .Include(r => r.ApplicationUser)
                .Include(r => r.Route)
                .ToListAsync();
        }

        public async Task Update(int id, Review review)
        {
            try
            {
                var existingReview = await Get(id);
                if(existingReview is null)
                {
                    throw new RecordNotFoundException($"Review id {id} not found in repository");
                }
                existingReview.Description = review.Description;
                existingReview.Score = review.Score;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}