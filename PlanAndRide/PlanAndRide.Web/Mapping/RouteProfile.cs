using AutoMapper;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Mapping
{
    public class RouteProfile : Profile
    {
        public RouteProfile()
        {
            CreateMap<BusinessLogic.Route, RouteDto>()
                .ReverseMap();
            CreateMap<RouteDto, RouteViewModel>()
                .ReverseMap();
            CreateMap<RouteDtoWithReviews, RouteReviewsViewModel>();

            CreateMap<BusinessLogic.Route, RouteDtoWithReviews>();

        //    CreateProjection<RouteDto, RouteReviewsViewModel>()
        //        .ForMember(model => model.Route, expr => expr.MapFrom(dto => dto.Name))
        //        .ForMember(model => model.Reviews, expr => expr.MapFrom(dto => dto.Reviews));
        }
    }
}
