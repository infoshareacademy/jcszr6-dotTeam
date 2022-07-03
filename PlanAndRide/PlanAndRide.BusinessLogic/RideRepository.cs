using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Environment = System.Environment;

namespace PlanAndRide.BusinessLogic
{
    public  class RideRepository: IRepository<Ride>
    {
        private List<Ride> _ride;
        public RideRepository()
        {
            _ride = new List<Ride>();
        }
        public Ride? Get(int id)
        {
            try
            {
                return _ride.FirstOrDefault(r => r.Id == id);
            }
            catch
            {
                throw new InvalidOperationException($"Unique key violaton: Ride Id:{id}");
            }
        }
        public IEnumerable<Ride> GetAll()
        {
            return _ride;
        }        
        public void Add(Ride ride)
        {
            if(_ride.Count > 0)
            {
                ride.Id = _ride.Max(r=>r.Id)+1;
            }
            else
            {
                ride.Id = 1;
            }
            _ride.Add(ride);
        }
        public void Update(int id, Ride ride)
        {
            var existingRide= Get(id);
            if(existingRide != null)
            {
                throw new RecordNotFoundException($"Ride Id:{id} not found in repository");
            }
            existingRide.Name = ride.Name;
            existingRide.Date = ride.Date;
            existingRide.IsPrivate = ride.IsPrivate;
            existingRide.Route = ride.Route;
        }
        public void Delete(int id)
        {
            _=_ride.Remove(Get(id));
        }
        public IEnumerable<Ride> FindByName(string name)
        {
            return _ride.Where(r=>r.Name.ToLower()==name.Trim().ToLower));
        }
        //private static List<Ride> rides = new List<Ride>();

        //static RideRepository()
        //{
        //    var json = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "data.json"));
        //    rides = JsonConvert.DeserializeObject<List<Ride>>(json);
        //}

        //public static List<Ride> GetAllRides()
        //{
        //    return rides;
        //}

        //public static void AddRide(Ride ride)
        //{
        //    rides.Add(ride);
        //}

    }
}
