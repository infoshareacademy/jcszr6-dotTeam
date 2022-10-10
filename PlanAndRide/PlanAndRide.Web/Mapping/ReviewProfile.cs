using AutoMapper;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Mapping
{
    public class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>()
                .ForMember(dto => dto.UserName, expr => expr.MapFrom(r => r.ApplicationUser.Login))
                .ForMember(dto => dto.RouteName, expr => expr.MapFrom(r => r.Route.Name))
                .ForMember(dto => dto.Date, expr => expr.MapFrom(r => r.Date.ToString("dd MMM yyyy")));
            CreateMap<CreateEditRouteReviewDto, Review>();
                
        }
    }
}
