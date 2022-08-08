using GeoCoordinatePortable;


namespace PlanAndRide.BusinessLogic
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GeoCoordinate StartingPosition { get; set; }
        public GeoCoordinate DestinationPosition { get; set; }
        public string StartingCity { get; set; }
        public string DestinationCity { get; set; }
        public string? Description { get; set; }
        public bool ShareRoute { get; set; }
        public bool IsPrivate { get; set; }
        public List<Review> Reviews { get; set; }
    }
    
}