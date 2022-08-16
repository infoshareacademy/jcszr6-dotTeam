using PlanAndRide.BusinessLogic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanAndRide.Web.Models
{
    public class RouteViewModel
    {
        private readonly IRouteService _routeService;
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
        public string StartingCity
        {
            get => Route.StartingCity;
            set { Route.StartingCity = value; }
        }
        public string DestinationCity
        {
            get => Route.DestinationCity;
            set { Route.DestinationCity = value; }
        }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
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
        public RouteViewModel(BusinessLogic.Route route, IRouteService routeService)
        {
            _routeService = routeService;
            Route = new BusinessLogic.Route
            {
                Id = route.Id,
                Name = route.Name,
                User = route.User,
                StartingPosition = new GeoCoordinate
                {
                    Latitude = route.StartingPosition.Latitude,
                    Longitude = route.StartingPosition.Longitude
                },
                DestinationPosition = new GeoCoordinate
                {
                    Latitude = route.DestinationPosition.Latitude,
                    Longitude = route.DestinationPosition.Longitude
                },
                StartingCity = route.StartingCity,
                DestinationCity = route.DestinationCity,
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
                Reviews = new List<Review>(),
            };
            _routeService = new RouteService();
        }
    }
}
