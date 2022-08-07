namespace PlanAndRide.BusinessLogic
{
    public class RideService:IRideService
    {
        private readonly IRepository<Ride> _repository;
        public RideService() { }
        public RideService(IRepository<Ride> repository)
        {
            _repository = repository;
        }
        
        public void Add(Ride ride)
        {
            _repository.Add(ride);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Ride Get(int id)
        {
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
        }

        public IEnumerable<Ride> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(int id, Ride ride)
        {
            try
            {
                _repository.Update(id, ride);
            }
            catch
            {
                throw;
            };
        }
        public IEnumerable<Ride> FindByName(string name)
        {
            return _repository.GetAll().Where(r => r.Name.ToLower().Contains(name.Trim().ToLower()));
        }
    }
}
