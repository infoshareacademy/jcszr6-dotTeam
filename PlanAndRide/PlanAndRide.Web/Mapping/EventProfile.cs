using AutoMapper;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Mapping
{
    public class EventProfile:Profile
    {
        public EventProfile()
        {
            CreateMap<Ride, EventDto>()
                .ForMember(model => model.Id, expr => expr.MapFrom(source => source.Id))
                .ForMember(model => model.Name, expr => expr.MapFrom(source => source.Name))
                .ForMember(model => model.Date, expr => expr.MapFrom(source => source.Date))
                .ForMember(model => model.Routes, expr=>expr.Ignore())
                .ForMember(model => model.RouteId, expr => expr.MapFrom(source => source.Route.Id.ToString()))
                .ForMember(model => model.RouteName, expr => expr.MapFrom(source => source.Route.Name))
                .ForMember(model => model.Description, expr => expr.MapFrom(source => source.Description))
                .ForMember(model => model.IsPrivate, expr => expr.MapFrom(source => source.IsPrivate))
                .ForMember(model => model.ShareRide, expr => expr.MapFrom(source => source.ShareRide))
                .ReverseMap();
        }
    }
}
