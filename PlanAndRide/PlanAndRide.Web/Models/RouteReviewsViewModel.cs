namespace PlanAndRide.Web.Models
{
    public class RouteReviewsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ReviewViewModel>? Reviews { get; set; }
    }
}
