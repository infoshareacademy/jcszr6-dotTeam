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
                //return AverageGradeRoute();
            }
        }
        public string? Description { get; set; }
        public bool ShareRoute { get; set; }
        public bool IsPrivate { get; set; }
        public List<Review> Reviews { get; set; }


        //public double AverageGradeRoute()
        //{

        //    //obliczanie średniej oceny dla wszystkich ocen z recenzji danej trasy
            
        //        foreach (var ride in RideRepository.GetAllRides())
        //        {
        //            List<int> listToSum = new List<int>();
        //            ride.Route.Reviews.ForEach(r => listToSum.Add(r.Score));
        //            int sum = listToSum.Sum();
        //            var averageGrade = sum / listToSum.Count;
        //        return averageGrade;
        //        }
        //    return 0d;
        //}

    }
    
}