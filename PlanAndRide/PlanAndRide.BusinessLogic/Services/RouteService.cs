using AutoMapper;
using X.PagedList;

namespace PlanAndRide.BusinessLogic
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _repository;
        private readonly IMapper _mapper;

        public RouteService(IRouteRepository repository, IMapper mapper)
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

        public async Task<IEnumerable<RouteDto>> GetByUser(string id)
        {
            var routes = await _repository.GetByUser(id);
            var dtos = _mapper.Map<IEnumerable<RouteDto>>(routes);
            var routesList = routes.ToList();
            var i = 0;
            foreach (var dto in dtos)
            {
                dto.AverageScore = AverageScore(routesList[i]);
                i++;
            }
            return dtos;
        }
        public async Task<IEnumerable<RouteDto>> GetPublicRoutes()
        {
            var routes = await _repository.GetPublicRoutes();
            var dtos = _mapper.Map<IEnumerable<RouteDto>>(routes);
            var routesList = routes.ToList();
            var i = 0;
            foreach (var dto in dtos)
            {
                dto.AverageScore = AverageScore(routesList[i]);
                i++;
            }
            return dtos;
        }

        public async Task<IEnumerable<RouteDto>> GetByRating(double minRating)
        {
            var routes = await GetAll();
            return routes.Where(r=>r.AverageScore>=minRating).ToList();
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
                routeDto.ReviewsCount = route.Reviews != null ? route.Reviews.Count:0;
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
            var avg = route.Reviews.Average(r => r.Score);

            return Math.Round(avg * 2) / 2;
        }

        public async Task<RouteDtoWithReviews?> GetRouteWithReviews(int id, string orderBy, int page, int pageSize)
        {
            try
            {
                var route = await _repository.Get(id);
                if (route is null)
                {
                    return null;
                }
                var routeDto = _mapper.Map<RouteDtoWithReviews>(route);
                routeDto.AverageScore = AverageScore(route);
                if (route.Reviews != null)
                {
                    
                    var orderedReviews = GetOrderedReviews(route.Reviews, orderBy);
                    var orderedReviewDtos = _mapper.Map<IList<ReviewDto>>(orderedReviews);
                    routeDto.PagedReviews = GetPagedReviews(orderedReviewDtos, page, pageSize);
                }
                return routeDto;
            }
            catch
            {
                throw;
            }
        }
        private IOrderedEnumerable<Review> GetOrderedReviews(IList<Review> reviews, string orderBy) =>
            orderBy switch
            {
                "score_asc" => reviews.OrderBy(dto => dto.Score),
                "score_desc" => reviews.OrderByDescending(dto => dto.Score),
                "date_asc" => reviews.OrderBy(dto => dto.Date),
                _ or "date_desc" => reviews.OrderByDescending(dto => dto.Date),

            };
        private IPagedList<ReviewDto> GetPagedReviews (IEnumerable<ReviewDto> reviews, int pageNumber, int pageSize)
        {
            var pagedReviews = reviews.ToPagedList(pageNumber, pageSize);
            var lastPageNumber = pagedReviews.PageCount;
            if(reviews.Any() && pageNumber > lastPageNumber)
            {
                pagedReviews = reviews.ToPagedList(lastPageNumber, pageSize);
            }
            return pagedReviews;
        }
        public async Task<string> GetRouteName(int id)
        {
            try
            {
                var route = await _repository.Get(id);
                if (route is null)
                {
                    return String.Empty;
                }
                return route.Name;
            }
            catch
            {
                throw;
            }
        }
    }
}