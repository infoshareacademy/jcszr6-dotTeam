using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Web.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public int User { get; set; }
        public string Route { get; set; }
        public int Score { get; set; }
        public  string Date { get; set; }
        public string Description { get; set; }
    }
}
