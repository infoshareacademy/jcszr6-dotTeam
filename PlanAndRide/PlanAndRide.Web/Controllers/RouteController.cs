using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;

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

            return View(_routeRepository.GetAll());
        }

        // GET: RouteController/Details/5
        public ActionResult Details(int id)
        {
            var route=_routeRepository.Get(id);
            if(route != null)
            {
                return View(route);
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
        public ActionResult Create(BusinessLogic.Route route)
        {
            ModelState.Remove("Reviews");
            if (!ModelState.IsValid)
            {
                return View(route);
            }
            if (_routeRepository.FindByName(route.Name) == null)
            {
                _routeRepository.Add(route);
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        // GET: RouteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RouteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
            return View();
        }

        // POST: RouteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
