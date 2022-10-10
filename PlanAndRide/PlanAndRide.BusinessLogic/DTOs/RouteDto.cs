using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class RouteDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public ApplicationUser? User { get; set; }
        [Required(ErrorMessage="Please enter a starting location")]
        public string? StartingLocation { get; set; }
        [Required(ErrorMessage = "Please enter a target location")]
        public string? DestinationLocation { get; set; }
        public GeoCoordinate? StartingPosition { get; set; }
        public GeoCoordinate? DestinationPosition { get; set; }
        public string? EncodedGoogleMapsPath { get; set; }
        public string? EncodedGoogleMapsWaypoints { get; set; }
        public string? StartingCity { get; set; }
        public string? DestinationCity { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public bool ShareRoute { get; set; }
        public bool IsPrivate { get; set; }
        public double AverageScore { get; set; }
        public int ReviewsCount { get; set; }
    }
}
