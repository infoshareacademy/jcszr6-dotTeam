using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Database.Repository
{
    public class RideRepository:IRepository<Ride>
    {
        private List<Ride> _rides;

        public RideRepository()
        {
            _rides= new List<Ride>();
        }
        public Ride Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ride> GetAll()
        {
            return _rides;
        }

        public void Add(Ride ride)
        {
            _rides.Add(ride);
        }

        public void Update(int id, Ride ride)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
