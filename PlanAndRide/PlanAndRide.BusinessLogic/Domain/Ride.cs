
using PlanAndRide.BusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;

namespace PlanAndRide.BusinessLogic
{
    public class Ride
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Name { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual IList<User> RideMembers { get; set; }

        public virtual  Route? Route{ get; set; }

        public string? Description { get; set; }

        public bool ShareRide { get; set; }

        public bool IsPrivate { get; set; }
        public virtual IList<UserRide> UserRide { get; set; }
        //public virtual IList<StatusList> StatusLists { get; set; }
       public int StatusRide { get; set; }

        
    }
}