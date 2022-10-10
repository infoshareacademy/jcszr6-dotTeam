using AutoMapper;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Mapping
{
    public class ClubProfile:Profile
    {
        public ClubProfile()
        {
            CreateMap<Club, ClubViewModel>()
                .ReverseMap();
        }
    }
}
