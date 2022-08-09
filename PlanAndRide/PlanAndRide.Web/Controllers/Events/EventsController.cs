using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Controllers.Events
{
    public class EventsController : Controller
    {
        private readonly IRideService _rideService;
        private readonly IRouteService _routeService;

        public EventsController(IRideService rideService, IRouteService routeService)
        {
            _routeService = routeService;
            _rideService = rideService;
        }
        // GET: EventsController
        public ActionResult Index()
        {
            var model = _rideService.GetAll().Select(r=>new EventViewModel
            {
                Id= r.Id,
                Name=r.Name,
                Date=r.Date,
                RouteId=r.Route.Id.ToString(),
                RouteName=r.Route.Name,
                Description=r.Description,
                ShareRide=r.ShareRide,
                IsPrivate=r.IsPrivate

            });
            return View(model);
        }

        // GET: EventsController/Details/5
        public ActionResult Details(int id)
        {
            var ride = _rideService.Get(id);
            if(ride!= null)
            {
                return View(ride);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: EventsController/Create
        public ActionResult Create()
        {
            var routes = _routeService.GetAll();
            var model = new EventViewModel() { Routes=routes};
            return View(model);
        }

        // POST: EventsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventViewModel model)
        {   //ModelState.Remove(nameof(ride.Route));
            if(!ModelState.IsValid)
            {
                model.Routes= _routeService.GetAll();
                return View(model);
            }
            var ride = new Ride
            {
                Name = model.Name,
                Date = model.Date,
                Description = model.Description,
                ShareRide = model.ShareRide,
                IsPrivate = model.IsPrivate
            };
            if(int.TryParse(model.RouteId,out int id))
                ride.Route= _routeService.Get(id);
                       
            _rideService.Add(ride);
            return RedirectToAction(nameof(Index));
        }

        // GET: EventsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Ride ride)
        {
            ModelState.Remove(nameof(ride.Route));
            if (!ModelState.IsValid)
            {
                return View(ride);
            }
            _rideService.Update(id,ride);
            return RedirectToAction(nameof(Index));
        }
    

        // GET: EventsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Ride ride)
        {
            _rideService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
