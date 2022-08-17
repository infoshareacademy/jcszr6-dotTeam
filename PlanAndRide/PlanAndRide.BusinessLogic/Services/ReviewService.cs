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
        public void Add(Review entity)
        {
            _repository.Add(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Review Get(int id)
        {
            try
            {
                return _repository.Get(id);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Review> GetAll()
        {
            return _repository.GetAll();
        }
        public IEnumerable<Review> GetByReferenceId(int referenceId, ReviewType type)
        {
            return _repository.GetAll().Where(r => r.ReferenceId == referenceId && r.Type == type);
        }

        public void Update(int id, Review entity)
        {
            try
            {
                _repository.Update(id, entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
