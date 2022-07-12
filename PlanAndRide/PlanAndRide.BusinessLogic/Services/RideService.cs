namespace PlanAndRide.BusinessLogic
{
    public class RideService:IRideService
    {
        private readonly IRepository<Ride> _repository;

        public RideService(IRepository<Ride> repository)
        {
            _repository = repository;
        }

        public void Add(Ride entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Ride Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ride> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(int id, Ride ride)
        {
            throw new NotImplementedException();
        }
    }
}
