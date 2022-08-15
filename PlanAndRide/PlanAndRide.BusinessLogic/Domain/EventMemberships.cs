using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanAndRide.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace PlanAndRide.BusinessLogic
{
    public class EventMemberships
    {   
        public int Id { get; set; }
       public User? User { get; set; }
       public string? Login { get; set; }
        public DateTime? DateOfJoin { get; set; }
        public string? Comment { get; set; }

    }
}
