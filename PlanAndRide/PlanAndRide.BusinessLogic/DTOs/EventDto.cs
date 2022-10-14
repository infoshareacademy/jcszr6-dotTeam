using PlanAndRide.BusinessLogic;
using System.ComponentModel.DataAnnotations;
namespace PlanAndRide.BusinessLogic
{
    public class EventDto
    {
        public int Id { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime Date { get; set; }
        public string? RouteId { get; set; }
        public string? RouteName { get; set; }

        public IEnumerable<RouteDto>? AvailableRoutes { get; set; }
        public RouteDto? Route { get; set; }

        public string? Description { get; set; }
        public bool IsViewerJoined { get; set; }

        public bool ShareRide { get; set; }
        public bool IsPrivate { get; set; }
        public string? StatusRide { get; set; }
    }
}