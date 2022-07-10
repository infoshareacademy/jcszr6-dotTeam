namespace PlanAndRide.Web.Models
{
    public class UserViewsModel
    {
        public string UserName { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
