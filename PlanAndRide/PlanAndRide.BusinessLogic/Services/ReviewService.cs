using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Add(ReviewDto entity)
        {
            await _repository.Add(_mapper.Map<Review>(entity));
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

        //public async Task<IEnumerable<Review>> GetAll()
        //{
        //    //return await _repository.GetAll();
        //}
        public async Task<IEnumerable<ReviewDto>> GetByRoute(int id)
        {
            var reviews = await _repository.GetByRoute(id);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task Update(int id, ReviewDto entity)
        {
            try
            {
                await _repository.Update(id, _mapper.Map<Review>(entity));
            }
            catch
            {
                throw;
            }
        }
    }
}
