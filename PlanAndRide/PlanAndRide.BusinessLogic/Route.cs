using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.BusinessLogic
{
    public class Route
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public double StartingPosition { get; set; }
        public double DestinationPosition { get; set; }
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
        public string Description { get; set; }
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