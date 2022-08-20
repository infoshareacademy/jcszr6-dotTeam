using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;
using System.Diagnostics;

namespace PlanAndRide.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RouteRepository _routeRepository;

        public HomeController(ILogger<HomeController> logger,RouteRepository repository)
        {
            _logger = logger;
            _routeRepository = repository;
        }

        public IActionResult Index()
        {
            var routes = _routeRepository.GetAll().OrderByDescending(r => r.Id).Take(3);
            var model = new RouteViewsModel();
            model.Routes = routes.Select(r => new RouteViewModel(r));
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