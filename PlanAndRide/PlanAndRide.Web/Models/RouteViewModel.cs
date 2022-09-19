using PlanAndRide.BusinessLogic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanAndRide.Web.Models
{
    public class RouteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GeoCoordinate StartingPosition { get; set; }
        public GeoCoordinate DestinationPosition { get; set; }
        public string StartingCity { get; set; }
        public string DestinationCity { get; set; }
        public string? EncodedGoogleMapsPath { get; set; }
        public string? EncodedGoogleMapsWaypoints { get; set; }
        public double AverageScore { get; set; }
        public string? Description { get; set; }
        public bool ShareRoute { get; set; }
        public bool IsPrivate { get; set; }
    }
}
