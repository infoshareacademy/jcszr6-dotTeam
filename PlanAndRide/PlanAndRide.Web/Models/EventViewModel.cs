using PlanAndRide.BusinessLogic;
using PlanAndRide.Database;
using System.ComponentModel.DataAnnotations;
namespace PlanAndRide.Web.Models
{
    public class EventViewModel
    {
        public BusinessLogic.Ride Ride { get; }

        public int Id
        {
            get { return Ride.Id; }
        }
        //[Required]
        public string Name
        {
            get { return Ride.Name; }
            set { Ride.Name = value; }
        }
        public DateTime? Date
        {
            get { return Ride.Date; }
            set { Ride.Date = value; }
        }
        public IEnumerable<BusinessLogic.Route> Routes { get; set; }

        public string? Description
        {
            get { return Ride.Description; }
            set { Ride.Description = value; }
        }
        public bool ShareRide
        {
            get { return Ride.ShareRide; }
            set { Ride.ShareRide = value; }
        }
        public bool IsPrivate
        {
            get { return Ride.IsPrivate; }
            set { Ride.IsPrivate = value; }
        }

        public EventViewModel(Ride ride)
        {
            Ride = new Ride
            {
                Id = ride.Id,
                Name = ride.Name,
                Description = ride.Description,
                ShareRide = ride.ShareRide,
                IsPrivate = ride.IsPrivate,

            };
        }
        public EventViewModel()
        {
            Ride = new Ride();
        }
    }
}
