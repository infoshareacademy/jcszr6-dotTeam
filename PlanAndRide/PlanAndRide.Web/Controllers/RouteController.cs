using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Controllers
{
    public class RouteController : Controller
    {
        private readonly RouteRepository _routeRepository;

        public RouteController(RouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }
        // GET: RouteController
        public ActionResult Index()
        {
            var routeViews = _routeRepository.GetAll().Select(x => new RouteViewModel(x));
            return View(routeViews);
        }

        // GET: RouteController/Details/5
        public ActionResult Details(int id)
        {
            var route = _routeRepository.Get(id);
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
        public ActionResult Create(RouteViewModel routeView)
        {
            if (!ModelState.IsValid)
            {
                return View(routeView);
            }
            _routeRepository.Add(routeView.GetRoute());
            return RedirectToAction(nameof(Index));
        }

        // GET: RouteController/Edit/5
        public ActionResult Edit(int id)
        {
            var route = _routeRepository.Get(id);
            if (route != null)
            {
                return View(new RouteViewModel(route));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: RouteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RouteViewModel routeView)
        {
            if (!ModelState.IsValid)
            {
                return View(routeView);
            }
            try
            {
                _routeRepository.Update(id, routeView.GetRoute());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RouteController/Delete/5
        public ActionResult Delete(int id)
        {
            var route = _routeRepository.Get(id);
            if (route != null)
            {
                return View(new RouteViewModel(route));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: RouteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RouteViewModel route)
        {
            _routeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: RouteController/Search
        public ActionResult Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var routes = _routeRepository.FindByName(name);
                var routeViews = routes.Select(r => new RouteViewModel(r));
                return View(nameof(Index), routeViews);
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
