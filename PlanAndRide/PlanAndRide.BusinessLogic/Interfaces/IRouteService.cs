namespace PlanAndRide.BusinessLogic
{
    public interface IRouteService
    {
        Task<IEnumerable<RouteDto>> GetAll();
        Task<IEnumerable<RouteDto>> GetByUser(string id);
        Task<RouteDto?> Get(int id);
        Task Add(RouteDto dto);
        Task Update(int id, RouteDto dto);
        Task Delete(int id);
        Task<IEnumerable<RouteDto>> FindByName(string name);
        double AverageScore(Route route);
        Task<RouteDtoWithReviews?> GetRouteWithReviews(int id, string orderBy,int page, int pageSize);
        Task<string> GetRouteName(int id);
        Task<IEnumerable<RouteDto>> GetByRating(double minRating);
    }
}