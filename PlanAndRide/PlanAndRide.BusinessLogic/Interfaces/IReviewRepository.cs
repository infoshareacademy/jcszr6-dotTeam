namespace PlanAndRide.BusinessLogic
{
    public interface IReviewRepository
    {
        Task Add(Review entity);
        Task Delete(int id);
        Task<Review?> Get(int id);
        Task<IEnumerable<Review>> GetAll();
        Task Update(int id, Review entity);

    }
}


