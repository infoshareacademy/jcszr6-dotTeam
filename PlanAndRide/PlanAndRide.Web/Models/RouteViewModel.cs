using GeoCoordinatePortable;
using PlanAndRide.BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace PlanAndRide.Web.Models
{
    public class RouteViewModel
    {
        private readonly RouteService _routeService;
        public BusinessLogic.Route Route { get; }
        public int Id
        {
            get { return Route.Id; }
        }
        [Required]
        public string Name
        {
            get {return Route.Name;}
            set { Route.Name = value; }
        }
        public GeoCoordinate StartingPosition
        {
            get {return Route.StartingPosition;}
            set { Route.StartingPosition = value; }
        }
        public GeoCoordinate DestinationPosition
        {
            get {return Route.DestinationPosition;}
            set { Route.DestinationPosition = value; }
        }
        public double AverageScore
        {
            get { return _routeService.AverageScore(Route); }
        }
        public string? Description
        {
            get { return Route.Description; }
            set { Route.Description = value; }
        }
        public bool ShareRoute
        {
            get { return Route.ShareRoute; }
            set { Route.ShareRoute = value; }
        }
        public bool IsPrivate
        {
            get { return Route.IsPrivate; }
            set { Route.IsPrivate = value; }
        }
        public RouteViewModel(BusinessLogic.Route route, RouteService routeService)
        {
            _routeService = routeService;
            Route = new BusinessLogic.Route
            {
                Id = route.Id,
                Name = route.Name,
                StartingPosition = new GeoCoordinate(route.StartingPosition.Latitude, route.StartingPosition.Longitude),
                DestinationPosition = new GeoCoordinate(route.DestinationPosition.Latitude, route.DestinationPosition.Longitude),
                Description = route.Description,
                ShareRoute = route.ShareRoute,
                IsPrivate = route.IsPrivate,
                Reviews = route.Reviews
            };
        }
        public RouteViewModel()
        {
            Route = new BusinessLogic.Route()
            { 
                Reviews=new List<Review>()
            };
            _routeService = new RouteService();
        }
    }
}
