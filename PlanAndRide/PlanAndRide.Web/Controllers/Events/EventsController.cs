using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;


namespace PlanAndRide.Web.Controllers.Events
{
    public class EventsController : Controller
    {
        private readonly RideRepository _rideRepository;

        public EventsController(RideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }
        // GET: EventsController
        public ActionResult Index()
        {
            return View(_rideRepository.GetAll());
        }

        // GET: EventsController/Details/5
        public ActionResult Details(int id)
        {
            var ride = _rideRepository.GetHashCode(id);
            if(ride!= ride)
            {
                return View(ride);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: EventsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BusinessLogic.Ride ride)
        {
            if(!ModelState.IsValid)
            {
                return View(ride);
            }
            _rideRepository.Add(ride);
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

        // GET: EventsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventsController/Delete/5
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
