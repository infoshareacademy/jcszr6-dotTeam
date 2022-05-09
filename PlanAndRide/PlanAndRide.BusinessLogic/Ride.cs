using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    class Ride
    {
        public string Name { get; set; }
        public DateTime Date { get; private set; }
        private readonly List<User> _rideMembers;
        private Route _route { get; set; }
        public string Description { get; set; }
        public bool ShareRide { get; set; }
        public bool IsPrivate { get; set; }
    }
}
