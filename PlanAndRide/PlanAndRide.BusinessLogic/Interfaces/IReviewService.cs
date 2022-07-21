
namespace PlanAndRide.BusinessLogic
{
    public interface IReviewService : IRepository<Review>
    {
        IEnumerable<Review> GetByReferenceId(int referenceId, ReviewType type);
    }
}
