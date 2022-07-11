namespace PlanAndRide.BusinessLogic
{
    public class RouteService
    {
        private readonly IRepository<Route> _repository;

        public RouteService(IRepository<Route> repository)
        {
            _repository = repository;
        }
        public IEnumerable<Route> GetAll()
        {
            return _repository.GetAll();
        }
        public Route? Get(int id)
        {
            try
            {
                return _repository.Get(id);
            }
            catch
            {
                throw new InvalidOperationException($"Unique key violaton: Route ID:{id}");
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

    }
}
