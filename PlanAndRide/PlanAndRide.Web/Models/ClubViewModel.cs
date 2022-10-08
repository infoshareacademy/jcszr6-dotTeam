using Microsoft.Build.Framework;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Web.Models
{
    public class ClubViewModel
    {
        public int Id { get; set; }
        public Club Club { get; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsPrivate { get; set; }

    }   
}
