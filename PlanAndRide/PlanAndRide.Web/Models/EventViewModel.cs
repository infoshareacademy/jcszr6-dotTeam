using PlanAndRide.BusinessLogic;
using PlanAndRide.Database;
using System.ComponentModel.DataAnnotations;
namespace PlanAndRide.Web.Models
{
    public class EventViewModel
    {

        //[Required]

        public string Name { get; set; }
 
        public DateTime? Date { get; set; }
        public BusinessLogic.Route? Route { get; set; }
        public IEnumerable<BusinessLogic.Route>? Routes { get; set; }

        public string Description { get; set; }

        public bool ShareRide { get; set; }
        public bool IsPrivate { get; set; }


        public EventViewModel(Ride ride)
        {
            Name = ride.Name;
            Date = ride.Date;
            Route=ride.Route;
            Description = ride.Description;
            ShareRide = ride.ShareRide;
            IsPrivate = ride.IsPrivate;
        }
        public EventViewModel()
        {

        }
    }
}
