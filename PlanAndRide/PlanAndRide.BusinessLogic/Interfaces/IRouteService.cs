namespace PlanAndRide.BusinessLogic
{
    public interface IRouteService :IRepository<Route>
    {
        IEnumerable<Route> FindByName(string name);
        double AverageScore(Route route);
    }
}