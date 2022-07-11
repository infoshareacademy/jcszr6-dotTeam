using GeoCoordinatePortable;
using Newtonsoft.Json;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Database.Repository
{
    public class RideRepository:IRepository<Ride>
    {
        private List<Ride> _rides;

        public RideRepository()
        {
            var json = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "TempDataFiles", "data.json"));
            _rides = JsonConvert.DeserializeObject<List<Ride>>(json);
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
