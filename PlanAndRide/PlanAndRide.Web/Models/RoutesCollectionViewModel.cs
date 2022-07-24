namespace PlanAndRide.Web.Models
{
    public class RoutesCollectionViewModel
    {
        public string? RouteName { get; set; }
        public IEnumerable<RouteViewModel>? Routes { get; set; }
    }
}
