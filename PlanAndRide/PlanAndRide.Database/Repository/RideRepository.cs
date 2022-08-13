using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Exceptions;


namespace PlanAndRide.Database.Repository
{
    public class RideRepository : IRepository<Ride>
    {
        private readonly PlanAndRideContext _context;
        //private static List<Ride>
        //     Rides = new List<Ride>
        //    {
        //        new Ride
        //        {
        //            Id = 1,
        //            Name ="Ride1",
        //            Date=DateTime.Now,
        //             Route= new Route
        //            {
        //                Name = "Route1"
        //            },
        //            Description="Ala ma kota",
        //            IsPrivate=true,
        //            ShareRide=true,
        //        },
        //        new Ride
        //        {
        //            Id = 2,
        //            Name ="Ride2",
        //            Date=DateTime.Now,
        //            Route= new Route
        //            {
        //                Name = "Route1"
        //            },
        //            Description="Kot ma Ale",
        //            IsPrivate=false,
        //            ShareRide=true,
        //        },
        //        new Ride
        //        {
        //            Id = 3,
        //            Name ="Ride3",
        //            Date=DateTime.Now,
        //            Route= new Route
        //            {
        //                Name = "Route2"
        //            },
        //            Description="Ale kod to kot",
        //            IsPrivate=true,
        //            ShareRide=true,
        //        }
        //    };


        public RideRepository(PlanAndRideContext context)
        {
            _context = context;
        }
        public async Task<Ride> Get(int id)
        {
            try
            {
                return await _context.Rides.SingleOrDefaultAsync(r => r.Id == id);   
            }
            catch
            {
                throw new InvalidOperationException($"Unique key violaton: Ride Id:{id}");
            }
        }
        public async Task<IEnumerable<Ride>> GetAll()
        {
            return await _context.Rides.ToListAsync();
        }
        public async Task Add(Ride ride)
        {
            await _context.Rides.AddAsync(ride);
            await _context.SaveChangesAsync();
        }
        public async Task Update(int id, Ride ride)
        {
            var existingRide = await Get(id);
            if (existingRide == null)
            {
                throw new RecordNotFoundException($"Ride Id:{id} not found in repository");
            }
            existingRide.Name = ride.Name;
            existingRide.Date = ride.Date;
            existingRide.IsPrivate = ride.IsPrivate;
            existingRide.ShareRide = ride.ShareRide;
            existingRide.Route = ride.Route;
            existingRide.Description = ride.Description;
            existingRide.RideMembers = ride.RideMembers;

            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var ride = await Get(id);
            _context.Rides.Remove(ride);
            await _context.SaveChangesAsync();
        }

               
        //static RideRepository()
        //{
        //    var json = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "data.json"));
        //    rides = JsonConvert.DeserializeObject<List<Route>>(json);
        //}

        //public static List<Route> GetAllRides()
        //{
        //    return rides;
        //}

        //public static void AddRide(Route route)
        //{
        //    route.Add(route);
        //}


    }
}
