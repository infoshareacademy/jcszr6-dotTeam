namespace PlanAndRide.BusinessLogic
{
    public interface IRideService
    {
       Task<IEnumerable<EventDto>> GetAll();
        Task<EventDto?> Get(int id);
        Task Add(EventDto dto);
        Task Update(int id,EventDto dto);
        Task Delete(int id);
    }
}