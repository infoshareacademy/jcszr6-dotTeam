using Microsoft.AspNetCore.Mvc;
using PlanAndRide.Web.Models;
using System.Diagnostics;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly RouteRepository _routeRepository;
        public HomeController(RouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }
        public IActionResult Index()
        {
            var model = new RouteViewsModel();
            model.Routes = _routeRepository.GetAll().Select(r => new RouteViewModel(r)).OrderByDescending(r => r.Id).Take(3);
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

        //public IActionResult LastThreeRoutes()
        //{
        //    return RedirectToAction("LastThreeRoutes", "Route");
        //}
    }
}