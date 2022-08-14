using PlanAndRide.BusinessLogic;
namespace PlanAndRide.Web.Models
{
    public class EventMembershipsViewModel
    { public IEnumerable<BusinessLogic.User>? EventMember { get; }
        
        public string Comment { get; set; }

        public DateTime JoinDate { get; set; }
        public EventMembershipsViewModel(User user)
            {
            
            Comment = string.Empty;
            JoinDate = DateTime.Now;
            }
        public EventMembershipsViewModel()
        {

        }
      
    }
}
