using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class Ride
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime? Date { get; set; }

        // do not change in edit
        public List<User> RideMembers;

        public string Route { get; set; }

        public string Description { get; set; }

        public bool ShareRide { get; set; }

        public bool IsPrivate { get; set; }

        public List<RouteRepository> Routes;
    }
}