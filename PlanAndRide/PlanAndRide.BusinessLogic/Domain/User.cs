namespace PlanAndRide.BusinessLogic
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Route> Routes { get; set; }
        public List<Ride> AttendedRides { get; set; }
        public List<Ride> CreatedRides { get; set; }
        public List<UserRide> UserRide { get; set; }
    }
}