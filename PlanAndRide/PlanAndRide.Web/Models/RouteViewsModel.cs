namespace PlanAndRide.Web.Models
{
    public class RouteViewsModel
    {
        public string? RouteName { get; set; }
        public IEnumerable<RouteViewModel>? Routes { get; set; }
    }
}
