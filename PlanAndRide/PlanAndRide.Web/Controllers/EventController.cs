using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Controllers.Events
{
    public class EventController : Controller
    {
        private readonly IRideService _rideService;
        private readonly IRouteService _routeService;
        private readonly IMapper _mapper;

        public EventController(IRideService rideService, IRouteService routeService, IMapper mapper)
        {
            _routeService = routeService;
            _mapper = mapper;
            _rideService = rideService;
        }
        // GET: EventsController
        public async Task<ActionResult> Index()
        {
            var rides = await _rideService.GetAll();
            var model = rides.Select(ride=>_mapper.Map<EventViewModel>(ride));
            return View(model);
        }

        // GET: EventsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var ride = await _rideService.Get(id);
            if(ride!= null)
            {
                return View(_mapper.Map<EventViewModel>(ride));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: EventsController/Create
        public async Task<ActionResult> Create()
        {
            var routes = await _routeService.GetAll();
            var model = new EventViewModel() { Routes=routes};
            return View(model);
        }

        // POST: EventsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventViewModel model)
        {   
            if(!ModelState.IsValid)
            {
                model.Routes= await _routeService.GetAll();
                return View(model);
            }
            
            var ride = _mapper.Map<Ride>(model);
            if (int.TryParse(model.RouteId, out int id))
                ride.Route = await _routeService.Get(id);
            else
                ride.Route = null;

            await _rideService.Add(ride);
            return RedirectToAction(nameof(Index));
        }

        // GET: EventsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var ride = await _rideService.Get(id);
            if (ride == null)
            {
                return RedirectToAction(nameof(Index));
            }
            
            var model = _mapper.Map<EventViewModel>(ride);
            model.Routes = await _routeService.GetAll();
            return View(model);
        }

        // POST: EventsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Routes = await _routeService.GetAll();
                return View(model);
            }

            var ride = _mapper.Map<Ride>(model);
            if (int.TryParse(model.RouteId, out int routeId))
                ride.Route = await _routeService.Get(routeId);
            else
                ride.Route = null;

            await _rideService.Update(id,ride);
            return RedirectToAction(nameof(Index));
        }
    

        // GET: EventsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var ride = await _rideService.Get(id);
            if (ride == null)
            {
                return RedirectToAction(nameof(Index));
            }

           return View(_mapper.Map<EventViewModel>(ride));
        }

        // POST: EventsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id,EventViewModel model)
        {
            await _rideService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
