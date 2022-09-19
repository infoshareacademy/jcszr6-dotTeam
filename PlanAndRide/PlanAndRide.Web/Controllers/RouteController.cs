using AutoMapper;
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
        private readonly IMapper _mapper;

        public RouteController(IRouteService routeService, IConfiguration config,IReviewService reviewService, IMapper mapper)
        {
            _routeService = routeService;
            _config = config;
            _reviewService = reviewService;
            _mapper = mapper;
        }
        // GET: RouteController
        public async Task<ActionResult> Index()
        {
            var model = new RoutesCollectionViewModel();
            var routes = await _routeService.GetAll();
            //model.Routes =routes.Select(r => new RouteViewModel(r,_routeService));
            model.Routes = _mapper.Map<IEnumerable<RouteViewModel>>(routes);
            return View(model);
        }

        // GET: RouteController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var route = await _routeService.Get(id);
            if (route is null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApiKey"] = _config["Maps:ApiKey"];
            return View(_mapper.Map<RouteViewModel>(route));
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
            var encodedWaypoints = model.EncodedGoogleMapsWaypoints;
            if (String.IsNullOrEmpty(encodedWaypoints) || String.IsNullOrWhiteSpace(encodedWaypoints))
            {
                model.EncodedGoogleMapsWaypoints = null;
            }

            //ModelState.Remove("Route.User");
            if (!ModelState.IsValid)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(model);
            }
            var routeDto = _mapper.Map<RouteDto>(model);
            routeDto.User = new User { Id = 1 };
            await _routeService.Add(routeDto);
            return RedirectToAction(nameof(Details), new {Id=model.Id});
        }

        // GET: RouteController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var route = await _routeService.Get(id);
            if (route != null)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(_mapper.Map<RouteViewModel>(route));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: RouteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, RouteViewModel model)
        {
            //ModelState.Remove("Route.User");
            if (!ModelState.IsValid)
            {
                ViewData["ApiKey"] = _config["Maps:ApiKey"];
                return View(model);
            }
            try
            {
                await _routeService.Update(id, _mapper.Map<RouteDto>(model));
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
                return View(_mapper.Map<RouteViewModel>(route));
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
                model.Routes = _mapper.Map<IEnumerable<RouteViewModel>>(routes);
                model.RouteName = routeName;
                return View(nameof(Index), model);
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<ActionResult> Reviews(int id)
        {
            var route = await _routeService.GetRouteWithReviews(id);
            if(route is null)
            {
                return RedirectToAction(nameof(Index));
            }
            var model = _mapper.Map<RouteReviewsViewModel>(route);
            return View(model);
        }
    }
}
