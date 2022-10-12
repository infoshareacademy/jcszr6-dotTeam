using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;
using System.Linq;

namespace PlanAndRide.Web.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteService _routeService;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        public RouteController(IRouteService routeService, IConfiguration config,UserManager<ApplicationUser> userManager)
        {
            _routeService = routeService;
            _config = config;
            _userManager = userManager;
        }
        // GET: RouteController
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var routes = await _routeService.GetAll();
            return View(routes);
        }
        [Authorize]
        public async Task<ActionResult> Rating(double? min)
        {
            var minRating = min ?? 0;
            ViewBag.MinRating = minRating;
            var routes = await _routeService.GetByRating(minRating);
            return View(nameof(Index),routes);
        }
        [Authorize]
        // GET: RouteController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var route = await _routeService.Get(id);
            if (route is null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApiKey"] = _config["Maps:ApiKey"];
            return View(route);
        }

        // GET: RouteController/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewData["ApiKey"] = _config["Maps:ApiKey"];
            return View();
        }

        // POST: RouteController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RouteDto route)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var encodedWaypoints = route.EncodedGoogleMapsWaypoints;
            if (String.IsNullOrEmpty(encodedWaypoints) || String.IsNullOrWhiteSpace(encodedWaypoints))
            {
                route.EncodedGoogleMapsWaypoints = null;
            }

            if (!ModelState.IsValid)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(route);
            }
            route.ApplicationUser = user;
            await _routeService.Add(route);
            return RedirectToAction(nameof(Details), new {Id=route.Id});
        }

        // GET: RouteController/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            var route = await _routeService.Get(id);
            if (route != null)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(route);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: RouteController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, RouteDto route)
        {
            //ModelState.Remove("Route.User");
            if (!ModelState.IsValid)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(route);
            }
            try
            {
                await _routeService.Update(id, route);
                return RedirectToAction(nameof(Details), new { Id=id });
            }
            catch
            {
                return View();
            }
        }

        // GET: RouteController/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var route = await _routeService.Get(id);
            if (route != null)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(route);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: RouteController/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, RouteDto route)
        {
            await _routeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: RouteController/Search
        [Authorize]
        public async Task<ActionResult> Search(string routeName)
        {
            if (!string.IsNullOrEmpty(routeName))
            {
                var routes = await _routeService.FindByName(routeName);
                var model = new RoutesCollectionViewModel
                {
                    RouteName = routeName,
                    Routes = routes

                };
                return View(nameof(Index), model);
            }
            return RedirectToAction(nameof(Index));

        }
        [Authorize]
        public async Task<ActionResult> Reviews(int id, string orderBy,int? page, int? pageSize)
        {
            var currentOrderBy = orderBy ?? "date_desc";
            var pageNumber = page ?? 1;
            var pageSizeNumber = pageSize ?? 5;
            var model = await _routeService.GetRouteWithReviews(id, currentOrderBy,pageNumber,pageSizeNumber);
            if(model is null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OrderBy = currentOrderBy;
            ViewBag.PageSize = pageSizeNumber;
            ViewBag.Page = model.PagedReviews.PageNumber;
            return View(model);
        }
        [Authorize]
        public async Task<ActionResult> ManageReviews(int id, string orderBy,int? page, int? pageSize)
        {
            var currentOrderBy = orderBy ?? "date_desc";
            var pageNumber = page ?? 1;
            var pageSizeNumber = pageSize ?? 5;
            var model = await _routeService.GetRouteWithReviews(id, currentOrderBy, pageNumber, pageSizeNumber);
            if (model is null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OrderBy = currentOrderBy;
            ViewBag.PageSize = pageSizeNumber;
            ViewBag.Page = model.PagedReviews.PageNumber;
            return View(model);
        }
    }
}

