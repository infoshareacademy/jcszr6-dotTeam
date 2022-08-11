
namespace PlanAndRide.BusinessLogic
{
    public interface IReviewService : IRepository<Review>
    {
        IEnumerable<Review> GetByRouteId(int id);
    }
}
