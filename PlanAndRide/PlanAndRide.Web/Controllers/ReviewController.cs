using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Database;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IRouteService _routeService;

        public ReviewController(IReviewService service,IRouteService routeService)
        {
            _reviewService = service;
            _routeService = routeService;
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
        public async Task<IActionResult> Details(int? id)
        {
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
            throw new NotImplementedException();
        }

        // GET: Reviews/Create
        public async Task<IActionResult> Create(int routeId)
        {
            //ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            //var model = new CreateRouteReviewDto { RouteId = routeId };
            ViewData["RouteName"] = await _routeService.GetRouteName(routeId);
            var model = new CreateRouteReviewDto { RouteId = routeId };
            return View(model);

        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteId,Score,Description")] CreateRouteReviewDto dto)
        {
            //    if (ModelState.IsValid)
            //    {
            //        _context.Add(review);
            //        await _context.SaveChangesAsync();
            //        return RedirectToAction(nameof(Index));
            //    }
            //    ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", review.RouteId);
            //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", review.UserId);
            //    return View(review);
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            dto.UserId = 1;
            dto.Date = DateTime.UtcNow;
            await _reviewService.Add(dto);
            return RedirectToAction(actionName:"Reviews",controllerName:"Route", new { Id = dto.RouteId });
                

                
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null || _context.Reviews == null)
            //{
            //    return NotFound();
            //}

            //var review = await _context.Reviews.FindAsync(id);
            //if (review == null)
            //{
            //    return NotFound();
            //}
            //ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", review.RouteId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", review.UserId);
            //return View(review);
            throw new NotImplementedException();
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,RouteId,Score,Date,Description")] Review review)
        {
            //if (id != review.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(review);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ReviewExists(review.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", review.RouteId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", review.UserId);
            //return View(review);
            throw new NotImplementedException();
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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
            throw new NotImplementedException();
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.Reviews == null)
            //{
            //    return Problem("Entity set 'PlanAndRideContext.Reviews'  is null.");
            //}
            //var review = await _context.Reviews.FindAsync(id);
            //if (review != null)
            //{
            //    _context.Reviews.Remove(review);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            throw new NotImplementedException();
        }

        private bool ReviewExists(int id)
        {
            //return (_context.Reviews?.Any(e => e.Id == id)).GetValueOrDefault();
            throw new NotImplementedException();
        }
    }
}
