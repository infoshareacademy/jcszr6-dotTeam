using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class RouteDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public User? User { get; set; }
        public GeoCoordinate? StartingPosition { get; set; }
        public GeoCoordinate? DestinationPosition { get; set; }
        public string? EncodedGoogleMapsPath { get; set; }
        public string? EncodedGoogleMapsWaypoints { get; set; }
        public string? StartingCity { get; set; }
        public string? DestinationCity { get; set; }
        public string? Description { get; set; }
        public bool ShareRoute { get; set; }
        public bool IsPrivate { get; set; }
        public double AverageScore { get; set; }
    }
}
