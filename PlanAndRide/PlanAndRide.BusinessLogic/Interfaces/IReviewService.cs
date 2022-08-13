
namespace PlanAndRide.BusinessLogic
{
    public interface IReviewService : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetByRouteId(int id);
    }
}
