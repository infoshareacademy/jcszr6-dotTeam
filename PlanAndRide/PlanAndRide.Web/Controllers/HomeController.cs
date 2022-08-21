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

        public HomeController(ILogger<HomeController> logger,IRouteService routeService)
        {
            _logger = logger;
            _routeService = routeService;
        }

        public async Task<IActionResult> Index()
        {
            var routes = await _routeService.GetAll();
            var lastThreeRoutes = routes.OrderByDescending(r => r.Id).Take(3).ToList();
            var model = new RoutesCollectionViewModel();
            model.Routes = lastThreeRoutes.Select(r => new RouteViewModel(r,_routeService));
            return View(model);
        }

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