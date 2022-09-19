using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class RouteDtoWithReviews
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ReviewDto> Reviews { get; set; }
    }
}
