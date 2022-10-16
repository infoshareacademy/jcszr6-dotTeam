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
    [Authorize]
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
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var routes = await _routeService.GetByUser(user.Id);
            ViewBag.ShowEditButtons = true;
            ViewBag.ShowPrivacySettings = true;
            return View(routes);
        }
        //public async Task<ActionResult> Public()
        //{
        //    var routes = await _routeService.GetPublicRoutes();
        //    ViewBag.ShowEditButtons = false;
        //    ViewBag.ShowPrivacySettings = false;
        //    return View(viewName: nameof(Index), model: routes);
        //}
        public async Task<ActionResult> Public(double? min)
        {
            var minRating = min ?? 0;
            var routes = await _routeService.GetPublicRoutes(minRating);
            ViewBag.ShowEditButtons = false;
            ViewBag.ShowPrivacySettings = false;
            return View(viewName: nameof(Index), model: routes);
        }
        public async Task<ActionResult> Rating(double? min)
        {
            var minRating = min ?? 0;
            ViewBag.MinRating = minRating;
            var routes = await _routeService.GetByRating(minRating);
            return View(nameof(Index),routes);
        }
        // GET: RouteController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var route = await _routeService.Get(id);
            if (route is null)
            {
                return RedirectToAction(nameof(Index));
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var isOwner = await IsOwner(id, user.Id);
            if (isOwner)
                ViewBag.ShowEditButtons = true;
            ViewBag.ReturnUrl = Request.Headers["Referer"].ToString();
            ViewData["ApiKey"] = _config["Maps:ApiKey"];
            return View(route);
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
        public async Task<ActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var route = await _routeService.Get(id);
            var isOwner = await IsOwner(id, user.Id);
            if (route == null || !isOwner)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApiKey"] = _config["Maps:ApiKey"];
            return View(route);
        }

        // POST: RouteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, RouteDto model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var isOwner = await IsOwner(id, user.Id);
            if(!isOwner)
            {
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(model);
            }
            try
            {
                await _routeService.Update(id, model);
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
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var route = await _routeService.Get(id);
            var isOwner = await IsOwner(id, user.Id);
            if (route == null || !isOwner)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApiKey"] = _config["Maps:ApiKey"];
            return View(route);
        }

        // POST: RouteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, RouteDto route)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var isOwner = await IsOwner(id, user.Id);
            if (!isOwner)
            {
                return RedirectToAction(nameof(Index));
            }
            await _routeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: RouteController/Search
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
        public async Task<ActionResult> Reviews(int id, string orderBy,int? page, int? pageSize)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var currentOrderBy = orderBy ?? "date_desc";
            var pageNumber = page ?? 1;
            var pageSizeNumber = pageSize ?? 5;
            var model = await _routeService.GetRouteWithReviews(id,currentUser.Id,currentOrderBy,pageNumber,pageSizeNumber);
            if(model is null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OrderBy = currentOrderBy;
            ViewBag.PageSize = pageSizeNumber;
            ViewBag.Page = model.PagedReviews.PageNumber;
            return View(model);
        }
        public async Task<ActionResult> ManageReviews(int id, string orderBy,int? page, int? pageSize)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var currentOrderBy = orderBy ?? "date_desc";
            var pageNumber = page ?? 1;
            var pageSizeNumber = pageSize ?? 5;
            var model = await _routeService.GetRouteWithReviews(id, currentUser.Id, currentOrderBy, pageNumber, pageSizeNumber);
            if (model is null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OrderBy = currentOrderBy;
            ViewBag.PageSize = pageSizeNumber;
            ViewBag.Page = model.PagedReviews.PageNumber;
            return View(model);
        }
        private async Task<bool> IsOwner(int routeId, string userId)
        {
            var route = await _routeService.Get(routeId);
            if (route == null || route.ApplicationUser == null || userId != route.ApplicationUser.Id)
            {
                return false;
            }
            return true;
        }
    }
}

