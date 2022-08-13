namespace PlanAndRide.BusinessLogic
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IList<Review> Reviews { get; set; }
        public IList<Route> Routes { get; set; }
        public IList<Ride> AttendedRides { get; set; }
        public IList<Ride> CreatedRides { get; set; }
        public IList<UserRide> UserRide { get; set; }
    }
}