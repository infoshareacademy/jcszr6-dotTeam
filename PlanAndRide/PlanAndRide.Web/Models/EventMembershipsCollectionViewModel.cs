namespace PlanAndRide.Web.Models
{
    public class EventMembershipsCollectionViewModel
    {
        public string? EventMembershipsName  { get; set; }
        public IEnumerable<EventMembershipsViewModel>? EventMemberships { get; set; }
    }
}
