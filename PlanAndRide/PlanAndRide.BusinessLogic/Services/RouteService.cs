using AutoMapper;

namespace PlanAndRide.BusinessLogic
{
    public class RouteService : IRouteService
    {
        private readonly IRepository<Route> _repository;
        private readonly IMapper _mapper;

        public RouteService(IRepository<Route> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RouteDto>> GetAll()
        {
            var routes = await _repository.GetAll();
            var dtos = _mapper.Map<IEnumerable<RouteDto>>(routes);
            var routesList = routes.ToList();
            int i = 0;
            foreach (var dto in dtos)
            {
                dto.AverageScore = AverageScore(routesList[i]);
                i++;
            }
            return dtos;
        }

        public async Task<RouteDto?> Get(int id)
        {
            Route route;
            try
            {
                route = await _repository.Get(id);
            }
            catch
            {
                throw;
            }
            var routeDto = _mapper.Map<RouteDto>(route);
            if (routeDto != null)
            {
                routeDto.AverageScore = AverageScore(route);
            }
            return routeDto;
        }

        public async Task Add(RouteDto dto)
        {
            var route = _mapper.Map<Route>(dto);
            await _repository.Add(route);
        }

        public async Task Update(int id, RouteDto dto)
        {
            try
            {
                var route = _mapper.Map<Route>(dto);
                await _repository.Update(id, route);
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<RouteDto>> FindByName(string name)
        {
            var routes = await _repository.GetAll();
            var dtos = _mapper.Map<IEnumerable<RouteDto>>(routes
                .Where(r => r.Name.ToLower().Contains(name.Trim().ToLower())));
            return dtos;
        }

        public double AverageScore(Route? route)
        {
            if (route is null || route.Reviews == null || route.Reviews.Count == 0)
            {
                return 0d;
            }
            var avg = route.Reviews.Select(r => r.Score).Average();
            return Math.Round(avg, 1);
        }

        public async Task<RouteDtoWithReviews?> GetRouteWithReviews(int id)
        {
            try
            {
                var route = await _repository.Get(id);
                return _mapper.Map<RouteDtoWithReviews>(route);
            }
            catch
            {
                throw;
            }
        }
    }
}