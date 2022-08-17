using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _repository;

        public ReviewService(IRepository<Review> repository)
        {
            _repository = repository;
        }
        public async Task Add(Review entity)
        {
            await _repository.Add(entity);
        }

        public async Task Delete(int id)
        {
           await _repository.Delete(id);
        }

        public async Task<Review> Get(int id)
        {
            try
            {
                return await _repository.Get(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<IEnumerable<Review>> GetByRouteId(int id)
        {
            var reviews = await _repository.GetAll();
            return reviews.Where(r => r.Route.Id==id);
        }

        public async Task Update(int id, Review entity)
        {
            try
            {
                await _repository.Update(id, entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
