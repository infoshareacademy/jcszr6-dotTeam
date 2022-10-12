using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IRouteService _routeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(IReviewService service, IRouteService routeService, UserManager<ApplicationUser> userManager)
        {
            _reviewService = service;
            _routeService = routeService;
            _userManager = userManager;
        }

        // GET: Reviews
        //public async Task<IActionResult> Index()
        //{
        //    //var planAndRideContext = _context.Reviews.Include(r => r.Route).Include(r => r.User);
        //    //return View(await planAndRideContext.ToListAsync());
        //    //var reviews = await _reviewService.GetByRoute(routeId);
        //    //if (!reviews.Any())
        //    //{
        //    //    return RedirectToAction("Index","Route",routeId);
        //    //}
        //    //var route = _mapper.Map<RouteViewModel>(reviews.First().Route);

        //    var model = await _reviewService.GetAll();
        //    return View(model);

        //}

        // GET: Reviews/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
            //if (id == null || _context.Reviews == null)
            //{
            //    return NotFound();
            //}

            //var review = await _context.Reviews
            //    .Include(r => r.Route)
            //    .Include(r => r.User)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (review == null)
            //{
            //    return NotFound();
            //}

            //return View(review);
        //}

        // GET: Reviews/Create
        public async Task<IActionResult> Create(int routeId)
        {
            var route = await _routeService.Get(routeId);
            if (route is null)
            {
                return NotFound();
            }
            TempData["RouteName"] = route.Name;
            var model = new CreateEditRouteReviewDto { RouteId = routeId };
            return View(model);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteId,Score,Description")] CreateEditRouteReviewDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            dto.UserId = user.Id;
            dto.Date = DateTime.UtcNow;
            await _reviewService.Add(dto);
            return RedirectToAction(actionName: "Reviews", controllerName: "Route", new { Id = dto.RouteId });
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _reviewService.Get(id);
            if(review is null)
            {
                return NotFound();
            }
            TempData["RouteName"] = review.RouteName;
            var model = new CreateEditRouteReviewDto
            {
                RouteId = review.RouteId,
                Score = review.Score,
                Description = review.Description
            };
            return View(model);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RouteId,Score,Description")] CreateEditRouteReviewDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _reviewService.Update(id,dto);
            return RedirectToAction(actionName: "ManageReviews", controllerName: "Route", new { Id = dto.RouteId });
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewService.Get(id);
            if(review is null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _reviewService.Get(id); 
            if(review is null)
            {
                return NotFound();
            }
            await _reviewService.Delete(id);
            return RedirectToAction(actionName: "ManageReviews", controllerName: "Route", new { Id = review.RouteId });
        }
    }
}