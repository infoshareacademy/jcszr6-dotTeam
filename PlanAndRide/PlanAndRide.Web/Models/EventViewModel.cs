using PlanAndRide.BusinessLogic;
using PlanAndRide.Database;
using System.ComponentModel.DataAnnotations;
namespace PlanAndRide.Web.Models
{
    public class EventViewModel
    {
        public BusinessLogic.Ride Ride { get; }
        public Database.Repository.RouteRepository RouteRepository { get; }
       
          
      public int Id
        {
            get { return Ride.Id; }
        }
        [Required]
      public string Name
        {
            get { return Ride.Name; }
            set { Ride.Name = value; }
        }
        public string? Description
        {
            get { return Ride.Description; }
            set { Ride.Description = value; }
        }
        public bool ShareRoute
        {
            get { return Ride.ShareRide; }
            set { Ride.ShareRide = value; }
        }
        public bool IsPrivate
        {
            get { return Ride.IsPrivate; }
            set { Ride.IsPrivate = value; }
        }
    }
}
