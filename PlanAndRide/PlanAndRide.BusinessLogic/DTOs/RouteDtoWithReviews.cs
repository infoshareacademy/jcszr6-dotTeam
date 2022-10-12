using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace PlanAndRide.BusinessLogic
{
    public class RouteDtoWithReviews
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AverageScore { get; set; }
        public bool ReviewedByCurrentUser { get; set; }
        public IPagedList<ReviewDto> PagedReviews { get; set; }
    }
}
