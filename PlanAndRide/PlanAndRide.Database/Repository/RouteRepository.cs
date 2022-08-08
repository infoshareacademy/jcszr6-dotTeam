using GeoCoordinatePortable;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Exceptions;

namespace PlanAndRide.Database.Repository
{
    public class RouteRepository : IRepository<Route>
    {
        private List<Route> _routes;
        private readonly IRepository<Review> _reviewRepository;

        public RouteRepository(IRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
            _routes = new List<Route>
        {
            new Route
            {
                Id=1,
                Name="Droga stu zakrętów",
                StartingPosition=new GeoCoordinate(50.441556,16.242764),
                DestinationPosition=new GeoCoordinate(50.504702,16.397086),
                StartingCity="Kudowa-Zdrój",
                DestinationCity="Radków",
                Description="najpopularniejsza trasa w Polsce",
                Reviews=_reviewRepository.GetAll().Where(r=>r.ReferenceId==1 && r.Type==ReviewType.ROUTE).ToList()
            },
            new Route
            {
                Id=2,
                Name="Przełęcz Przysłup",
                StartingPosition=new GeoCoordinate(49.531141,22.300324),
                DestinationPosition=new GeoCoordinate(49.577351,22.369551),
                StartingCity="Załuż",
                DestinationCity="Tyrawa Wołoska",
                Description="najbardziej kręta",
                Reviews=_reviewRepository.GetAll().Where(r=>r.ReferenceId==2 && r.Type==ReviewType.ROUTE).ToList()
            },
            new Route
            {
                Id=3,
                Name="Droga Oswalda Balzera",
                StartingPosition=new GeoCoordinate(49.299042,19.949059),
                DestinationPosition=new GeoCoordinate(49.264127,20.115313),
                StartingCity="Zakopane",
                DestinationCity="Prešovský kraj",
                Description="Z Zakopanego do Morskiego Oka",
                Reviews=_reviewRepository.GetAll().Where(r=>r.ReferenceId==3 && r.Type==ReviewType.ROUTE).ToList()
            },
            new Route
            {
                Id=4,
                Name="Na Wielkiej Pętli Bieszczadzkej",
                StartingPosition=new GeoCoordinate(49.473736,22.325125),
                DestinationPosition=new GeoCoordinate(49.106629,22.650325),
                StartingCity="Lesko",
                DestinationCity="Ustrzyki Górne",
                Reviews=_reviewRepository.GetAll().Where(r=>r.ReferenceId==4 && r.Type==ReviewType.ROUTE).ToList()
            },
            new Route
            {
                Id=5,
                Name="Autostrada Sudecka",
                StartingPosition=new GeoCoordinate(50.397840,16.34907),
                DestinationPosition=new GeoCoordinate(50.147772,16.666961),
                StartingCity="Zielone Ludowe",
                DestinationCity="Międzylesie",
                Reviews=_reviewRepository.GetAll().Where(r=>r.ReferenceId==5 && r.Type==ReviewType.ROUTE).ToList()
            },
            new Route
            {
                Id=6,
                Name="Szlak Orlich Gniazd",
                StartingPosition=new GeoCoordinate(50.749408,19.271176),
                DestinationPosition=new GeoCoordinate(50.453674,19.551179),
                StartingCity="Olsztyn",
                DestinationCity="Ogrodzieniec",
                Reviews=_reviewRepository.GetAll().Where(r=>r.ReferenceId==6 && r.Type==ReviewType.ROUTE).ToList()
            }
        };
            
        }
        public Route Get(int id)
        {
            try
            {
                return _routes.SingleOrDefault(r => r.Id == id);
            }
            catch
            {
                throw new InvalidOperationException($"Unique key violaton: Route ID:{id}");
            }
        }
        public IEnumerable<Route> GetAll()
        {
            return _routes;
        }
        public void Add(Route route)
        {
            if (_routes.Count > 0)
            {
                route.Id = _routes.Max(r => r.Id) + 1;
            }
            else
            {
                route.Id = 1;
            }
            _routes.Add(route);
        }
        public void Update(int id,Route route)
        {
            var existingRoute = Get(id);
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

        }

        public void Delete(int id)
        {
            _ = _routes.Remove(Get(id));
        }
    }
}
