using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Controllers
{
    public class EventMembershipsController : Controller
    {
        var model = new EventMembershipsViewModel();
        // GET: EventMembershipsController
        public ActionResult Index()
        {
            
            return View(model);
        }

        // GET: EventMembershipsController/Details/5
        public ActionResult Details()
        {
            return View(model);
        }

        // GET: EventMembershipsController/Create
        public ActionResult Create()
        {
            return View(model);
        }

        // POST: EventMembershipsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: EventMembershipsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventMembershipsController/Edit/5
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

        // GET: EventMembershipsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventMembershipsController/Delete/5
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
