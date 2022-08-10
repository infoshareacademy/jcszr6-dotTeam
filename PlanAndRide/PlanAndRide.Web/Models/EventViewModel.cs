using PlanAndRide.BusinessLogic;
using PlanAndRide.Database;
using System.ComponentModel.DataAnnotations;
namespace PlanAndRide.Web.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
 
        public DateTime? Date { get; set; }
        public string? RouteId { get; set; }
        public string? RouteName { get; set; }

        public IEnumerable<BusinessLogic.Route>? Routes { get; set; }

        public string? Description { get; set; }

        public bool ShareRide { get; set; }
        public bool IsPrivate { get; set; }


        public EventViewModel(Ride ride)
        {
            Name = ride.Name;
            Date = ride.Date;
            RouteId=ride.Route.Id.ToString();
            Description = ride.Description;
            ShareRide = ride.ShareRide;
            IsPrivate = ride.IsPrivate;
        }
        public EventViewModel()
        {

        }
    }
}
