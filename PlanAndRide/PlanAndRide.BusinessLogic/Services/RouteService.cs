namespace PlanAndRide.BusinessLogic
{
    public class RouteService : IRouteService
    {
        private readonly IRepository<Route> _repository;
        public RouteService() { }                     
        public RouteService(IRepository<Route> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Route>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<Route> Get(int id)
        {
            try
            {
                return await _repository.Get(id);
            }
            catch
            {
                throw;
            }
        }
        public async Task Add(Route route)
        {
            await _repository.Add(route);
        }
        public async Task Update(int id, Route route)
        {
            try
            {
               await _repository.Update(id, route);
            }
            catch
            {
                throw;
            }
        }
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
        public async Task<IEnumerable<Route>> FindByName(string name)
        {
            var routes = await _repository.GetAll();
            return routes.Where(r => r.Name.ToLower().Contains(name.Trim().ToLower()));
        }
        public double AverageScore(Route route)
        {
            if (route.Reviews == null || route.Reviews.Count == 0)
            {
                return 0d;
            }
            return route.Reviews.Select(r => r.Score).Average();
        }

    }
}
