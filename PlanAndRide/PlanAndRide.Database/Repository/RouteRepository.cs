using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Exceptions;

namespace PlanAndRide.Database.Repository
{
    public class RouteRepository : IRepository<Route>
    {
        private readonly IRepository<Review> _reviewRepository;
        private readonly PlanAndRideContext _context;

        public RouteRepository(IRepository<Review> reviewRepository, PlanAndRideContext context)
        {
            _reviewRepository = reviewRepository;
            _context = context;
        }
        public async Task<Route> Get(int id)
        {
            try
            {
                return await _context.Routes
                    .Include(r => r.User)
                    .Include(r => r.StartingPosition)
                    .Include(r => r.DestinationPosition)
                    .SingleOrDefaultAsync(r => r.Id == id);
            }
            catch
            {
                throw new InvalidOperationException($"Unique key violaton: Route ID:{id}");
            }
        }
        public async Task<IEnumerable<Route>> GetAll()
        {
            return await _context.Routes
               .Include(r => r.User)
               .Include(r => r.StartingPosition)
               .Include(r => r.DestinationPosition)
               .ToListAsync();
        }
        
        public async Task Add(Route route)
        {
            var startingPosition = GetExistingGeoCoordinate(route.StartingPosition.Latitude, route.StartingPosition.Longitude);
            if(startingPosition != null)
                route.StartingPosition = startingPosition;
            
            var destinationPosition = GetExistingGeoCoordinate(route.DestinationPosition.Latitude, route.DestinationPosition.Longitude);
            if (destinationPosition != null)
                route.DestinationPosition = destinationPosition;
            
            var user = _context.Users.FirstOrDefault(u => u.Id == route.User.Id);
            if (user == null)
                throw new ArgumentException("User not found at route create");
            route.User = user;

            await _context.Routes.AddAsync(route);
            _context.SaveChanges();
        }

        private GeoCoordinate? GetExistingGeoCoordinate(double latitude, double longitude)
        {
            return _context.GeoCoordinates
                .FirstOrDefault(g => g.Latitude == latitude && g.Longitude == longitude);
        }

        public async Task Update(int id, Route route)
        {
            //var existingRoute = Get(id);
            //if (existingRoute == null)
            //{
            //    throw new RecordNotFoundException($"Route ID:{id} not found in repository");
            //}
            //existingRoute.Name = route.Name;
            //existingRoute.StartingPosition = route.StartingPosition;
            //existingRoute.DestinationPosition = route.DestinationPosition;
            //existingRoute.StartingCity = route.StartingCity;
            //existingRoute.DestinationCity = route.DestinationCity;
            //existingRoute.Description = route.Description;
            //existingRoute.ShareRoute = route.ShareRoute;
            //existingRoute.IsPrivate = route.IsPrivate;

            var existingRoute = await Get(id);
            if (existingRoute == null)
            {
                throw new RecordNotFoundException($"Route ID:{id} not found in repository");
            }
            existingRoute.Name = route.Name;
            existingRoute.StartingPosition = route.StartingPosition;
            existingRoute.DestinationPosition = route.DestinationPosition;
            existingRoute.StartingCity = route.StartingCity;
            existingRoute.DestinationCity = route.DestinationCity;
            existingRoute.Description = route.Description;
            existingRoute.ShareRoute = route.ShareRoute;
            existingRoute.IsPrivate = route.IsPrivate;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                var route = await _context.Routes.SingleOrDefaultAsync(u => u.Id == id);
                _context.Routes.Remove(route);
                _context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Unique key violaton: Route ID:{id}");
            }
            

        }
    }
}
