using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;



namespace PlanAndRide.Web.Controllers
{
    public class RouteController : Controller
    {
        private readonly RouteService _routeService;
        public RouteController(RouteService routeService)
        {
            _routeService = routeService;
        }
        // GET: RouteController
        public ActionResult Index()
        {
            var model = new RouteViewsModel();
            model.Routes = _routeService.GetAll().Select(r => new RouteViewModel(r));
            return View(model);
        }

        // GET: RouteController/Details/5
        public ActionResult Details(int id)
        {
            var route = _routeService.Get(id);
            if (route != null)
            {
                return View(new RouteViewModel(route));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: RouteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RouteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RouteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _routeService.Add(model.Route);
            return RedirectToAction(nameof(Search), new {routeName=model.Name});
        }

        // GET: RouteController/Edit/5
        public ActionResult Edit(int id)
        {
            var route = _routeService.Get(id);
            if (route != null)
            {
                return View(new RouteViewModel(route));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: RouteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RouteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                _routeService.Update(id, model.Route);
                return RedirectToAction(nameof(Details), new { Id=id });
            }
            catch
            {
                return View();
            }
        }

        // GET: RouteController/Delete/5
        public ActionResult Delete(int id)
        {
            var route = _routeService.Get(id);
            if (route != null)
            {
                return View(new RouteViewModel(route));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: RouteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RouteViewModel model)
        {
            _routeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: RouteController/Search
        public ActionResult Search(string routeName)
        {
            if (!string.IsNullOrEmpty(routeName))
            {
                var routes = _routeService.FindByName(routeName);
                var model = new RouteViewsModel();
                model.Routes = routes.Select(r => new RouteViewModel(r));
                model.RouteName = routeName;
                return View(nameof(Index), model);
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
