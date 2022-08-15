using AutoMapper;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Mapping
{
    public class EventMembershipsProfile: Profile
    {
        public EventMembershipsProfile()
        {
            CreateMap<EventMemberships, EventMembershipsViewModel>()
               .ForMember(model=>model.Id, expr =>expr.MapFrom(source=>source.Id))
               //.ForMember(model=>model.UserLogin, expr=>expr.MapFrom(source=>source.User.Login))
               .ForMember(model => model.Login, expr => expr.MapFrom(source => source.Login))
               .ForMember(model=>model.Comment, expr=>expr.MapFrom(source=>source.Comment))
               .ForMember(model=>model.DateOfJoin, expr=>expr.MapFrom(source=>source.DateOfJoin))
               .ReverseMap();
        }
    }
}
