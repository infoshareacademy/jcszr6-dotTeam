namespace PlanAndRide.BusinessLogic
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual IList<Review> Reviews { get; set; }
        public virtual IList<Route> Routes { get; set; }
        public virtual IList<Ride> AttendedRides { get; set; }
        public virtual IList<Ride> CreatedRides { get; set; }
        public virtual IList<UserRide> UserRide { get; set; }
    }
}