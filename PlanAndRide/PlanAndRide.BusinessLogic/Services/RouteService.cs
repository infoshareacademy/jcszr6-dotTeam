namespace PlanAndRide.BusinessLogic
{
    public class RouteService
    {
        private readonly IRepository<Route> _repository;
        public RouteService()
        {

        }
        public RouteService(IRepository<Route> repository)
        {
            _repository = repository;
        }
        public IEnumerable<Route> GetAll()
        {
            return _repository.GetAll();
        }
        public Route Get(int id)
        {
            try
            {
                return _repository.Get(id);
            }
            catch
            {
                throw;
            }
        }
        public void Add(Route route)
        {
            _repository.Add(route);
        }
        public void Update(int id, Route route)
        {
            try
            {
                _repository.Update(id, route);
            }
            catch
            {
                throw;
            }
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        public IEnumerable<Route> FindByName(string name)
        {
            return _repository.GetAll().Where(r => r.Name.ToLower().Contains(name.Trim().ToLower()));
        }
        public double AverageScore(Route route)
        {
            if (route.Reviews.Count == 0 || route.Reviews==null)
            {
                return 0d;
            }
            return route.Reviews.Select(r => r.Score).Average();
        }

    }
}
