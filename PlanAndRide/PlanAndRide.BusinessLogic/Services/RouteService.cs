using AutoMapper;

namespace PlanAndRide.BusinessLogic
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _repository;
        private readonly IMapper _mapper;

        public RouteService() { }                     
        public RouteService(IRouteRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RouteDto>> GetAll()
        {
            var routes = await _repository.GetAll();
            return _mapper.Map<IEnumerable<RouteDto>>(routes);
        }
        public async Task<RouteDto> Get(int id)
        {
            try
            {
                var route = await _repository.Get(id);
                var routeDto = _mapper.Map<RouteDto>(route);
                routeDto.AverageScore = AverageScore(route);
                return routeDto;
            }
            catch
            {
                throw;
            }
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
        public double AverageScore(Route route)
        {
            if (route.Reviews == null || route.Reviews.Count == 0)
            {
                return 0d;
            }
            var avg = route.Reviews.Select(r => r.Score).Average();
            return Math.Round(avg, 1);
        }
        public async Task<RouteDtoWithReviews> GetRouteWithReviews(int id)
        {
            var routes = await _repository.GetRouteWithReviews(id);
            return routes.FirstOrDefault();
        }
    }
}
