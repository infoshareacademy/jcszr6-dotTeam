using Microsoft.AspNetCore.Identity;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.BusinessLogic
{
    public class ApplicationUser : IdentityUser
    {
        //public int Id { get; set; }
        public string? Login { get; set; }
        public string? FirstName { get; set; }
        //public string Password { get; set; }
        //public string Email { get; set; }
        public virtual IList<Review> Reviews { get; set; }
        public virtual IList<Route> Routes { get; set; }
        public virtual IList<Ride> AttendedRides { get; set; }
        public virtual IList<Ride> CreatedRides { get; set; }
        public virtual IList<UserRide> UserRide { get; set; }
        public virtual IList<UserClub> UserClubs { get; set; }
        public virtual IList<Club> CreatedClubs { get; set; }
        public int UsernameChangeLimit { get; set; } = 20;
        public byte[]? ProfilePicture { get; set; }
        public string? BikeName { get; set; }
    }
}