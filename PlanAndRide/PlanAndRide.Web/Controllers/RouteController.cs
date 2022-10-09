using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;
using System.Linq;

namespace PlanAndRide.Web.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteService _routeService;
        private readonly IConfiguration _config;
        private readonly IReviewService _reviewService;

        public RouteController(IRouteService routeService, IConfiguration config,IReviewService reviewService)
        {
            _routeService = routeService;
            _config = config;
            _reviewService = reviewService;
        }
        // GET: RouteController
        public async Task<ActionResult> Index()
        {
            var model = new RoutesCollectionViewModel();
            var routes = await _routeService.GetAll();
            model.Routes =routes.Select(r => new RouteViewModel(r,_routeService));
            return View(model);
        }

        // GET: RouteController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var route = await _routeService.Get(id);
            if (route != null)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(new RouteViewModel(route,_routeService));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: RouteController/Create
        public ActionResult Create()
        {
            ViewData["ApiKey"] = _config["Maps:ApiKey"];
            return View();
        }

        // POST: RouteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RouteViewModel model)
        {
            model.Route.ApplicationUser = new ApplicationUser { Id = 1 };
            var encodedWaypoints = model.EncodedGoogleMapsWaypoints;
            if (String.IsNullOrEmpty(encodedWaypoints) || String.IsNullOrWhiteSpace(encodedWaypoints))
            {
                model.EncodedGoogleMapsWaypoints = null;
            }
            ModelState.Remove("Route.User");
            if (!ModelState.IsValid)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(model);
            }
            await _routeService.Add(model.Route);
            return RedirectToAction(nameof(Details), new {Id=model.Route.Id});
        }

        // GET: RouteController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var route = await _routeService.Get(id);
            if (route != null)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(new RouteViewModel(route, _routeService));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: RouteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, RouteViewModel model)
        {
            ModelState.Remove("Route.User");
            if (!ModelState.IsValid)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(model);
            }
            try
            {
                await _routeService.Update(id, model.Route);
                return RedirectToAction(nameof(Details), new { Id=id });
            }
            catch
            {
                return View();
            }
        }

        // GET: RouteController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var route = await _routeService.Get(id);
            if (route != null)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(new RouteViewModel(route, _routeService));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: RouteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, RouteViewModel model)
        {
            await _routeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: RouteController/Search
        public async Task<ActionResult> Search(string routeName)
        {
            if (!string.IsNullOrEmpty(routeName))
            {
                var routes = await _routeService.FindByName(routeName);
                var model = new RoutesCollectionViewModel();
                model.Routes = routes.Select(r => new RouteViewModel(r,_routeService));
                model.RouteName = routeName;
                return View(nameof(Index), model);
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<ActionResult> Reviews(int routeId)
        {
            var route = await _routeService.Get(routeId);
            var model = new RouteViewModel(route,_routeService);
            return View(model);
        }
    }
}
