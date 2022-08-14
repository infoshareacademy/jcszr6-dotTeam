using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class UserRide
    {
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual int RideId { get; set; }
        public virtual Ride Ride { get; set; }
    }
}
