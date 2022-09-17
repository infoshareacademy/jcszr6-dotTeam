using PlanAndRide.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class Club
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public bool ShareRide { get; set; }

        public bool IsPrivate { get; set; }

        public virtual IList<Review> Reviews { get; set; }

        public virtual IList<Route> Routes { get; set; }

        public virtual IList<Ride> AttendedRides { get; set; }

        public virtual IList<UserClub> UserClubs { get; set; }

        public virtual IList<ApplicationUser> ClubMembers { get; set; }

    }
}
