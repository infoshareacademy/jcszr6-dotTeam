﻿
using System.ComponentModel.DataAnnotations;

namespace PlanAndRide.BusinessLogic
{
    public class Ride
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Name { get; set; }

        public virtual DateTime? Date { get; set; }

        public virtual IList<ApplicationUser> RideMembers { get; set; }

        public virtual  Route? Route{ get; set; }

        public string? Description { get; set; }

        public bool ShareRide { get; set; }

        public bool IsPrivate { get; set; }
        public virtual IList<UserRide> UserRide { get; set; }

        
    }
}