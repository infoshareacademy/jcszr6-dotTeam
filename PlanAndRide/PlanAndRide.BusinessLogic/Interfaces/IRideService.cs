
namespace PlanAndRide.BusinessLogic
{
    public interface IRideService
    {
        Task<IEnumerable<EventDto>> GetAll();
        Task<EventDto?> Get(int id,string memberId);
        Task<IEnumerable<EventDto>> GetByUser(string id);
        Task<IEnumerable<EventDto>> GetPublic();
        Task Add(EventDto dto);
        Task Update(int id, EventDto dto);
        Task Delete(int id);
        Task AddRideMember(int rideId, string memberId);
        Task RemoveRideMember(int rideId, string memberId);
    }
}