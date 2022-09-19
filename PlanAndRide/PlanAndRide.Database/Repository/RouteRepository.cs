﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Exceptions;

namespace PlanAndRide.Database.Repository
{
    public class RouteRepository : IRouteRepository
    {
        //private readonly IReviewRepository _reviewRepository;
        private readonly PlanAndRideContext _context;
        private readonly IMapper _mapper;

        public RouteRepository(PlanAndRideContext context, IMapper mapper)
        {
            //_reviewRepository = reviewRepository;
            _context = context;
            _mapper = mapper;
        }
        public async Task<Route> Get(int id)
        {
            try
            {
                
                return await _context.Routes
                    //.Include(r => r.User)
                    //.Include(r => r.StartingPosition)
                    //.Include(r => r.DestinationPosition)
                    //.Include(r => r.Reviews)
                    .SingleOrDefaultAsync(r => r.Id == id);
            }
            catch
            {
                throw new InvalidOperationException($"Unique key violaton: Route ID:{id}");
            }
        }
        public async Task<IEnumerable<RouteDtoWithReviews>> GetRouteWithReviews(int id)
        {
            return await _context.Routes
                .Where(r => r.Id == id).ProjectTo<RouteDtoWithReviews>(_mapper.ConfigurationProvider).ToListAsync();
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
            if (startingPosition != null)
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
            Route existingRoute;
            try
            {
                existingRoute = await _context.Routes.SingleOrDefaultAsync(r => r.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Unique key violaton: Route ID:{id}");
            }

            if (existingRoute == null)
            {
                throw new RecordNotFoundException($"Route ID:{id} not found in repository");
            }
            existingRoute.Name = route.Name;


            var existingPosition = GetExistingGeoCoordinate(route.StartingPosition.Latitude, route.StartingPosition.Longitude);
            if (existingPosition == null)
            {
                existingRoute.StartingPosition = route.StartingPosition;
            }
            else
            {
                existingRoute.StartingPosition = existingPosition;
            }

            existingPosition = GetExistingGeoCoordinate(route.DestinationPosition.Latitude, route.DestinationPosition.Longitude);
            if (existingPosition == null)
            {
                existingRoute.DestinationPosition = route.DestinationPosition;
                
            }
            else
            {
                existingRoute.DestinationPosition = existingPosition;
            }
            
            existingRoute.StartingCity = route.StartingCity;
            existingRoute.DestinationCity = route.DestinationCity;
            existingRoute.Description = route.Description;
            existingRoute.ShareRoute = route.ShareRoute;
            existingRoute.IsPrivate = route.IsPrivate;
            existingRoute.EncodedGoogleMapsPath = route.EncodedGoogleMapsPath;
            existingRoute.EncodedGoogleMapsWaypoints = route.EncodedGoogleMapsWaypoints;

            _context.SaveChanges();
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
