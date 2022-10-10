using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class UserClub
    {
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }
    }
}
