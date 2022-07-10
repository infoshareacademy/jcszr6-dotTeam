using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Web.Models
{
    public class UserViewModel
    {
        private BusinessLogic.User User { get; }
        public int Id
        {
            get { return User.Id; }
        }

        public string Login
        {
            get { return User.Login; }
            set { User.Login = value; }
        }

        public string Password
        {
            get { return User.Password; }
            set { User.Password = value; }
        }

        public string Email
        {
            get { return User.Email; }
            set { User.Email = value; }
        }

        public UserViewModel(BusinessLogic.User user)
        {
            User = new BusinessLogic.User
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
            };
        }
        public UserViewModel()
        {
            User = new BusinessLogic.User();
        }
    }
}