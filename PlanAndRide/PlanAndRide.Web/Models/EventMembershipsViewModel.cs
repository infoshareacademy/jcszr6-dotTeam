using PlanAndRide.BusinessLogic;
namespace PlanAndRide.Web.Models
{
    public class EventMembershipsViewModel
    {
        private readonly IEventMembershipsService _eventMembershipsService;
        public BusinessLogic.EventMemberships? EventMemberships { get; }
        //public IEnumerable<User>? EventMember { get; }
        //public int Id { get { return EventMemberships.Id; } }
        //public string Login { get { return EventMemberships.Login; } }
        //public string? Comment { get { return EventMemberships.Comment; } }
        //public DateTime? DateOfJoin { get { return EventMemberships.DateOfJoin; } }
        public int Id { get; set; }
        public string Login { get; set; }
        public string? Comment { get; set; }
        public DateTime? DateOfJoin { get; set; }
        public EventMembershipsViewModel(User user, EventMemberships eventMemberships, IEventMembershipsService eventMembershipsService)
            {
            _eventMembershipsService = eventMembershipsService;
            eventMemberships = new EventMemberships
            {
                Id = eventMemberships.Id,
                Login = eventMemberships.Login,
                //UserLogin = user.Login;
                Comment = eventMemberships.Comment,
                DateOfJoin = eventMemberships.DateOfJoin,
            };
            
            }
        public EventMembershipsViewModel()
        {

        }
      
    }
}
