using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Web.Models
{
    public class RoutesCollectionViewModel
    {
        public string? RouteName { get; set; }
        public IEnumerable<RouteDto>? Routes { get; set; }
    }
}
