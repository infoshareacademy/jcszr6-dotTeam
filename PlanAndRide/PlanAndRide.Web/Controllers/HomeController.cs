﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Database;
using PlanAndRide.Web.Models;
using System.Diagnostics;

namespace PlanAndRide.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRouteService _routeService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,IRouteService routeService, IConfiguration config,IMapper mapper)
        {
            _logger = logger;
            _routeService = routeService;
            _config = config;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var routes = await _routeService.GetAll();
            var lastThreeRoutes = routes.OrderByDescending(r => r.Id).Take(3).ToList();
            var model = new RoutesCollectionViewModel();
            model.Routes = lastThreeRoutes;
            return View(model);
        }

        //public async Task<ActionResult> Details(int id)
        //{
        //    var route = await _routeService.GetAll();
        //    var lastRoute = route.OrderByDescending(r => r.Id).Take(1);

        //        ViewData["ApiKey"] = _config["Maps:ApiKey"];
        //        return View(new RouteViewModel(route, _routeService));
 
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}