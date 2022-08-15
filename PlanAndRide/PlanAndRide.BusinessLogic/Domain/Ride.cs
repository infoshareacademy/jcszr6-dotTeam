
using PlanAndRide.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace PlanAndRide.BusinessLogic
{
    public class Ride
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime? Date { get; set; }

        public List<EventMemberships>? RideMembers;

        public  Route? Route{ get; set; }

        public string? Description { get; set; }

        public bool ShareRide { get; set; }

        public bool IsPrivate { get; set; }

        
    }
}