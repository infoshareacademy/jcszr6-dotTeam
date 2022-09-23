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
        public async Task Add(CreateEditRouteReviewDto entity)
        {
            await _repository.Add(_mapper.Map<Review>(entity));
        }

        public async Task Delete(int id)
        {
           await _repository.Delete(id);
        }

        public async Task<ReviewDto> Get(int id)
        {
            try
            {
                var review = await _repository.Get(id);
                return _mapper.Map<ReviewDto>(review);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ReviewDto>> GetAll()
        {
            var reviews =  await _repository.GetAll();
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }
        //public async Task<IEnumerable<ReviewDto>> GetByRoute(int id)
        //{
        //    var reviews = await _repository.GetByRoute(id);
        //    return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        //}

        public async Task Update(int id, CreateEditRouteReviewDto entity)
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
