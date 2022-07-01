using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoCoordinatePortable;

namespace PlanAndRide.BusinessLogic
{
    public class RouteRepository : IRepository<Route>
    {
        private List<Route> _routes;
        public RouteRepository()
        {
            _routes = new List<Route>
            {
                new Route
                {
                    Id=1,
                    Name="Route1",
                    StartingPosition=new GeoCoordinate(25.251,36.325),
                    DestinationPosition=new GeoCoordinate(36.855,69.654),
                    Reviews=new List<Review>()
                },                
                new Route
                {
                    Id=2,
                    Name="Route2",
                    StartingPosition=new GeoCoordinate(54.213,37.325),
                    DestinationPosition=new GeoCoordinate(36.855,97.554),
                    Reviews=new List<Review>()
                },
                new Route
                {
                    Id=3,
                    Name="R3",
                    StartingPosition=new GeoCoordinate(54.213,37.325),
                    DestinationPosition=new GeoCoordinate(36.855,97.554),
                    Reviews=new List<Review>()
                }

            };
        }
        public Route? Get(int id)
        {
            try
            {
                return _routes.SingleOrDefault(r => r.Id == id);
            }
            catch
            {
                throw new InvalidOperationException($"Unique key violaton: Route ID:{id}");
            }
        }
        public IEnumerable<Route> GetAll()
        {
            return _routes;
        }
        public void Add(Route route)
        {
            if (_routes.Count > 0)
            {
                route.Id = _routes.Max(r => r.Id) + 1;
            }
            else
            {
                route.Id = 1;
            }
            _routes.Add(route);
        }
        public void Update(int id, Route route)
        {
            var existingRoute = Get(id);
            if (existingRoute == null)
            {
                throw new RecordNotFoundException($"Route ID:{id} not found in repository");
            }
            existingRoute.Name = route.Name;
            existingRoute.StartingPosition = route.StartingPosition;
            existingRoute.DestinationPosition = route.DestinationPosition;
            existingRoute.Description = route.Description;
            existingRoute.ShareRoute = route.ShareRoute;
            existingRoute.IsPrivate = route.IsPrivate;

        }

        public void Delete(int id)
        {
           _ = _routes.Remove(Get(id));
        }
        public IEnumerable<Route> FindByName(string name)
        {
            return _routes.Where(r => r.Name.ToLower().Contains(name.Trim().ToLower()));
        }


    }
}
