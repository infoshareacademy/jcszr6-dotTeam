using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class UserRide
    {
        public  string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public  int RideId { get; set; }
        public virtual Ride Ride { get; set; }
    }
}
