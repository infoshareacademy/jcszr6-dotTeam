namespace PlanAndRide.BusinessLogic
{
    public interface IRouteService
    {
        Task<IEnumerable<RouteDto>> GetAll();
        Task<RouteDto> Get(int id);
        Task Add(RouteDto dto);
        Task Update(int id, RouteDto dto);
        Task Delete(int id);
        Task<IEnumerable<RouteDto>> FindByName(string name);
        double AverageScore(Route route);
        Task<RouteDtoWithReviews> GetRouteWithReviews(int id);
    }
}