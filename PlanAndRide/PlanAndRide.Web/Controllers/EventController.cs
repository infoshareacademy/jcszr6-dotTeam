using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Controllers.Events
{
    public class EventController : Controller
    {
        private readonly IRideService _rideService;
        private readonly IRouteService _routeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventController(IRideService rideService, IRouteService routeService,UserManager<ApplicationUser> userManager)
        {
            _routeService = routeService;
            _userManager = userManager;
            _rideService = rideService;
        }
        // GET: EventsController
        public async Task<ActionResult> Index()
        {
            var rides = await _rideService.GetAll();
            return View(rides);
        }

        // GET: EventsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var ride = await _rideService.Get(id);
            if (ride == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(ride);
        }

        // GET: EventsController/Create
        public async Task<ActionResult> Create()
        {
            var routes = await _routeService.GetAll();
            var model = new EventDto() { AvailableRoutes = routes };
            return View(model);
        }
        
        // POST: EventsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int Id,EventDto eventDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            eventDto.ApplicationUser = user;
            if (!ModelState.IsValid)
            {
                eventDto.AvailableRoutes = await _routeService.GetAll();
                return View(eventDto);
            }
            if (int.TryParse(eventDto.RouteId, out int id))
                eventDto.Route = await _routeService.Get(id) ;
            else
                eventDto.Route = null;

            await _rideService.Add(eventDto);
            return RedirectToAction(nameof(Index));
        }

        // GET: EventsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var ride = await _rideService.Get(id);
            if (ride != null)
            {
                ride.AvailableRoutes = await _routeService.GetAll();
                return View(ride);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: EventsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EventDto eventDto)
        {
            if (!ModelState.IsValid)
            {
                //eventDto.Routes = await _routeService.GetAll();
                return View(eventDto);
            }


            if (int.TryParse(eventDto.RouteId, out int routeId))
                eventDto.Route = await _routeService.Get(id);
            else
                eventDto.Route = null;

            await _rideService.Update(id, eventDto);
            return RedirectToAction(nameof(Index));
        }


        // GET: EventsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var ride = await _rideService.Get(id);
            if (ride != null)
            {
                return View(ride);
            }


            return RedirectToAction(nameof(Index));
        }

        // POST: EventsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, EventDto eventDto)
        {
            await _rideService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
