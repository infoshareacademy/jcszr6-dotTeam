using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic.Domain
{
    public class EventMemberships
    {   
        public int Id { get; set; }
        public List<User> Users { get;  }
        public string Nickname { get; set; }
        public DateTime DateOfJoin { get; set; }
        public string Comment { get; set; }

    }
}
