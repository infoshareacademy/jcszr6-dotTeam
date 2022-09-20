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
                .ForMember(dto => dto.UserName, expr => expr.MapFrom(r => r.User.Login))
                .ForMember(dto => dto.Route, expr => expr.MapFrom(r => r.Route.Name))
                .ForMember(dto => dto.Date, expr => expr.MapFrom(r => r.Date.ToString("d")));
            CreateMap<CreateRouteReviewDto, Review>();
                
        }
    }
}
