namespace PlanAndRide.Web.Models
{
    public class ClubsCollectionViewModel
    {
        public string? ClubName { get; set; }
        public IEnumerable<ClubViewModel>? Clubs { get; set; }
    }
}
