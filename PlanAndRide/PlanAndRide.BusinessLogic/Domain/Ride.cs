
using System.ComponentModel.DataAnnotations;

namespace PlanAndRide.BusinessLogic
{
    public class Ride
    {
        [Required]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime? Date { get; set; }

        public List<User> RideMembers;

        public  Route Route{ get; set; }

        public string Description { get; set; }

        public bool ShareRide { get; set; }

        public bool IsPrivate { get; set; }

        
    }
}