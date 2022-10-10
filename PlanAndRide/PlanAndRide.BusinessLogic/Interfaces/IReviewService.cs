
namespace PlanAndRide.BusinessLogic
{
    public interface IReviewService
    {
        Task Add(CreateEditRouteReviewDto entity);
        Task Delete(int id);
        Task<ReviewDto> Get(int id);
        Task<IEnumerable<ReviewDto>> GetAll();
        Task Update(int id, CreateEditRouteReviewDto entity);
    }
}
