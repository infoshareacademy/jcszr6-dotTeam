﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IConfiguration _config;

        public EventController(
            IRideService rideService, 
            IRouteService routeService,
            UserManager<ApplicationUser> userManager,
            IConfiguration config)
        {
            _routeService = routeService;
            _userManager = userManager;
            _config = config;
            _rideService = rideService;
        }
        // GET: EventsController
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var rides = await _rideService.GetByUser(user.Id);
            ViewBag.ShowEditButtons = true;
            ViewBag.ShowOwnerName = false;
            return View(rides);
        }
        // GET: EventsController
        [Authorize]
        public async Task<ActionResult> Public()
        {
            var rides = await _rideService.GetPublic();
            ViewBag.ShowEditButtons = false;
            ViewBag.ShowOwnerName = true;
            return View(viewName:nameof(Index),model:rides);
        }

        // GET: EventsController/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var ride = await _rideService.Get(id,user.Id);
            if (ride == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var isOwner = await IsOwner(id, user.Id);
            if (isOwner)
                ViewBag.ShowEditButtons = true;
            ViewBag.ReturnUrl = Request.Headers["Referer"].ToString();
            ViewData["ApiKey"] = _config["Maps:ApiKey"];
            return View(ride);
        }

        // GET: EventsController/Create
        [Authorize]
        public async Task<ActionResult> Create()
        {
            var routes = await _routeService.GetAll();
            var dateTimeNow = DateTime.Now;
            var dateTimeNowWithoutSeconds = dateTimeNow.AddTicks(-(dateTimeNow.Ticks % TimeSpan.TicksPerMinute));
            var model = new EventDto() { AvailableRoutes = routes, Date = dateTimeNowWithoutSeconds };
            return View(model);
        }
        
        // POST: EventsController/Create
        [HttpPost]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var ride = await _rideService.Get(id, user.Id);
            var isOwner = await IsOwner(id, user.Id);
            if (ride == null || !isOwner)
            {
                return RedirectToAction(nameof(Index));
            }
            ride.AvailableRoutes = await _routeService.GetAll();
            return View(ride);
        }

        // POST: EventsController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EventDto eventDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var isOwner = await IsOwner(id, user.Id);
            if (!isOwner)
            {
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                //eventDto.Routes = await _routeService.GetAll();
                return View(eventDto);
            }


            if (int.TryParse(eventDto.RouteId, out int routeId))
                eventDto.Route = await _routeService.Get(routeId);
            else
                eventDto.Route = null;

            await _rideService.Update(id, eventDto);
            return RedirectToAction(nameof(Index));
        }


        // GET: EventsController/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var ride = await _rideService.Get(id, user.Id);
            var isOwner = await IsOwner(id, user.Id);
            if (ride == null || !isOwner)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(ride);
        }

        // POST: EventsController/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, EventDto eventDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var isOwner = await IsOwner(id, user.Id);
            if (!isOwner)
            {
                return RedirectToAction(nameof(Index));
            }
            await _rideService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        // GET: EventsController/Create
        //[Authorize]
        //public async Task<ActionResult> AddMember()
        //{
        //    var routes = await _routeService.GetAll();
        //    var model = new EventDto() { AvailableRoutes = routes };
        //    return View(model);
        //}

        [Authorize]
        public async Task<ActionResult> Join(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _rideService.AddRideMember(id, user.Id);
            return RedirectToAction(nameof(Details), new {Id=id});
        }
        [Authorize]
        public async Task<ActionResult> Unjoin(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = await _rideService.Get(id, user.Id);
            if (model == null)
            {
                return RedirectToAction(nameof(Details), new { Id = id });
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Unjoin(int id, EventDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _rideService.RemoveRideMember(id, user.Id);
            return RedirectToAction(nameof(Details), new { Id = id });
        }
        private async Task<bool> IsOwner(int rideId, string userId)
        {
            var ride = await _rideService.Get(rideId, userId);
            if (ride == null || ride.ApplicationUser == null || userId != ride.ApplicationUser.Id)
            {
                return false;
            }
            return true;
        }
    }
}
