
namespace PlanAndRide.BusinessLogic
{
    public interface IReviewService
    {
        Task Add(ReviewDto entity);
        Task Delete(int id);
        Task<Review> Get(int id);
        Task<IEnumerable<ReviewDto>> GetByRoute(int id);
        Task Update(int id, ReviewDto entity);
    }
}
