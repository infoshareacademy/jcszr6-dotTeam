using Microsoft.Build.Framework;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Web.Models
{
    public class ClubViewModel
    {
        private readonly IClubService _clubService;
        public Club Club { get; }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool ShareRide { get; set; }
        public bool IsPrivate { get; set; }

        public ClubViewModel(Club club, IClubService clubService)
        {
            _clubService = clubService;
            Club = new Club
            {
                Name = club.Name,
                Description = club.Description,
                IsPrivate = club.IsPrivate
            };
        }
        public ClubViewModel()
        {
            _clubService = new ClubService();
        }
    }   
}
