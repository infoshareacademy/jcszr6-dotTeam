using GeoCoordinatePortable;


namespace PlanAndRide.BusinessLogic
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GeoCoordinate StartingPosition { get; set; }
        public GeoCoordinate DestinationPosition { get; set; }
        public double AverageScore
        {
            get
            {
                if(Reviews==null || Reviews.Count == 0)
                {
                    return 0d;
                }
                return Reviews.Select(review => review.Score).Average();
            }
        }
        public string? Description { get; set; }
        public bool ShareRoute { get; set; }
        public bool IsPrivate { get; set; }
        public List<Review> Reviews { get; set; }
    }
    
}