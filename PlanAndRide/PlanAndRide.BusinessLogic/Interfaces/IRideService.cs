
namespace PlanAndRide.BusinessLogic
{
    public interface IRideService
    {
        Task<IEnumerable<EventDto>> GetAll();
        Task<EventDto?> Get(int id,string userId);
        Task Add(EventDto dto);
        Task Update(int id, EventDto dto);
        Task Delete(int id);
        Task AddRideMember(int rideId, string userId);
        Task RemoveRideMember(int rideId, string userId);
    }
}