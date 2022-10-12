using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public interface IRouteRepository : IRepository<Route>
    {
        Task<IEnumerable<Route>> GetByUser(string id);
        Task<IEnumerable<Route>> GetPublicRoutes();
       

        
    }
}
