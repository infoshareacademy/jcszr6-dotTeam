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
            CreateMap<BusinessLogic.Route, RouteDtoWithReviews>();
        }
    }
}
