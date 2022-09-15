using AutoMapper;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Mapping
{
    public class RouteProfile:Profile
    {
        public RouteProfile()
        {
            CreateMap<BusinessLogic.Route, RouteDto>()
                .ForMember(dto => dto.AverageScore, expr => expr.MapFrom(r => MapRouteScore(r)))
                .ReverseMap();
            CreateMap<RouteDto, RouteViewModel>()
                .ReverseMap();

        }
        private double MapRouteScore(BusinessLogic.Route route)
        {
            var _routeService = new RouteService();
            return _routeService.AverageScore(route);
        }
    }
}
