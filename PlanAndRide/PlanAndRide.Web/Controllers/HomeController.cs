using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;
using System.Diagnostics;

namespace PlanAndRide.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRouteService _routeService;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger,IRouteService routeService, IConfiguration config)
        {
            _logger = logger;
            _routeService = routeService;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var routes = await _routeService.GetAll();
            var lastThreeRoutes = routes.OrderByDescending(r => r.Id).Take(3).ToList();
            var model = new RoutesCollectionViewModel();
            model.Routes = lastThreeRoutes.Select(r => new RouteViewModel(r,_routeService));
            return View(model);
        }

        //public async Task<ActionResult> Details(int id)
        //{
        //    var route = await _routeService.GetAll();
        //    var lastRoute = route.OrderByDescending(r => r.Id).Take(1);

        //        ViewData["ApiKey"] = _config["Maps:ApiKey"];
        //        return View(new RouteViewModel(route, _routeService));
 
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}