namespace PlanAndRide.BusinessLogic
{
    public interface IRepository<T> where T : class
    {

        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);
    }
}
