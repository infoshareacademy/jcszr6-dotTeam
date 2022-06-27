using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class RouteRepository : IRepository<Route>
    {
        private List<Route> _routes;
        public RouteRepository()
        {
            _routes = new List<Route>();
        }
        public Route? Get(int id)
        {
            return _routes.SingleOrDefault(r => r.Id == id);
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
                throw new InvalidOperationException("Not Found");
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
        public Route? FindByName(string name)
        {
            return _routes.FirstOrDefault(r => r.Name == name.Trim());
        }


    }
}
