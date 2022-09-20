using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public  string UserName { get; set; }
        public  string Route { get; set; }
        public int Score { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }
}
