using PlanAndRide.BusinessLogic;
using System.ComponentModel.DataAnnotations;
namespace PlanAndRide.BusinessLogic
{
    public class EventDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime Date { get; set; }
        public string? RouteId { get; set; }
        public string? RouteName { get; set; }

        //public IEnumerable<BusinessLogic.Route>? Routes { get; set; }
        public Route Route { get; set; }

        public string? Description { get; set; }

        public bool ShareRide { get; set; }
        public bool IsPrivate { get; set; }
        public string? StatusRide { get; set; }
    }
}