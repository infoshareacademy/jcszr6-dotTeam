

namespace PlanAndRide.BusinessLogic
{
    public class Route
    {
        public int Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Name { get; set; }
        public virtual GeoCoordinate? StartingPosition { get; set; }
        public virtual GeoCoordinate? DestinationPosition { get; set; }
        public string? StartingCity { get; set; }
        public string? DestinationCity { get; set; }
        public string? Description { get; set; }
        public bool ShareRoute { get; set; }
        public bool IsPrivate { get; set; }
        public virtual IList<Review> Reviews { get; set; }
    }
    
}