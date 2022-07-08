using GeoCoordinatePortable;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Web.Models
{
    public class RouteViewModel
    {
        public BusinessLogic.Route Route { get; }
        public int Id
        {
            get { return Route.Id; }
        }
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
            get {return Route.AverageScore;}
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
        public RouteViewModel(BusinessLogic.Route route)
        {
            Route = new BusinessLogic.Route
            {
                Id = route.Id,
                Name = route.Name,
                StartingPosition = new GeoCoordinate(route.StartingPosition.Latitude, route.StartingPosition.Longitude),
                DestinationPosition = new GeoCoordinate(route.DestinationPosition.Latitude, route.DestinationPosition.Longitude),
                Description = route.Description,
                ShareRoute = route.ShareRoute,
                IsPrivate = route.IsPrivate,
                Reviews = new List<Review>()
            };
        }
        public RouteViewModel()
        {
            Route = new BusinessLogic.Route();
            Route.Reviews = new List<Review>();
        }
    }
}
