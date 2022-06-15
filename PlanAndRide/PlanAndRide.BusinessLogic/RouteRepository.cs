using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class RouteRepository
    {
        private List<Route> _routes;

        public void AddRoute(Route route)
        {
            _routes.Add(route);
        }
        public List<Route> GetAllRoutes()
        {
            return _routes;
        }
    }
}
