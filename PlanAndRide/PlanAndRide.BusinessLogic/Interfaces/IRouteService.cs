namespace PlanAndRide.BusinessLogic
{
    public interface IRouteService :IRepository<Route>
    {
        Task<IEnumerable<Route>> FindByName(string name);
        double AverageScore(Route route);
    }
}