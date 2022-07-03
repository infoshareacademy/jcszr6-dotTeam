using GeoCoordinatePortable;

namespace PlanAndRide.Web.Models
{
    public class RouteViewModel
    {
        private BusinessLogic.Route _route;
        //public BusinessLogic.Route Route { get;}
        public int Id
        {
            get { return _route.Id; }
        }
        public string Name
        {
            get {return _route.Name;}
            set { _route.Name = value; }
        }
        public GeoCoordinate StartingPosition
        {
            get {return _route.StartingPosition;}
            set { _route.StartingPosition = value; }
        }
        public GeoCoordinate DestinationPosition
        {
            get {return _route.DestinationPosition;}
            set { _route.DestinationPosition = value; }
        }
        public double AverageScore
        {
            get {return _route.AverageScore;}
        }
        public string? Description
        {
            get { return _route.Description; }
            set { _route.Description = value; }
        }
        public bool ShareRoute
        {
            get { return _route.ShareRoute; }
            set { _route.ShareRoute = value; }
        }
        public bool IsPrivate
        {
            get { return _route.IsPrivate; }
            set { _route.IsPrivate = value; }
        }
        public RouteViewModel(BusinessLogic.Route route)
        {
            _route = route;
        }
        public RouteViewModel()
        {
            _route = new BusinessLogic.Route();
        }
        public BusinessLogic.Route GetRoute()
        {
            return _route;
        }
    }
}
