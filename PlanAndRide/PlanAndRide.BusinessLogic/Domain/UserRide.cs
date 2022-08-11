using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class UserRide
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int RideId { get; set; }
        public virtual Ride Ride { get; set; }
    }
}
