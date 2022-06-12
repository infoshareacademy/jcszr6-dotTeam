using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.BusinessLogic
{
    public class Route
    {
        

        public string Name { get; set; }
        public double StartingPosition { get; set; }
        public double DestinationPosition { get; set; }
        public double Score
        {
            get
            {
                return AverageGradeRoute();
            }
        }
        public string Description { get; set; }
        public bool ShareRoute { get; set; }
        public bool IsPrivate { get; set; }
        public List<Review> Reviews { get; set; }
        public double AverageGradeRoute()
        {

            //obliczanie średniej oceny dla wszystkich ocen z recenzji danej trasy
            
                foreach (var ride in RideRepository.GetAllRides())
                {
                    List<int> listToSum = new List<int>();
                    ride.Route.Reviews.ForEach(r => listToSum.Add(r.Score));
                    int sum = listToSum.Sum();
                    var averageGrade = sum / listToSum.Count;
                return averageGrade;
                }
            return 0d;
        }
    }
    
}