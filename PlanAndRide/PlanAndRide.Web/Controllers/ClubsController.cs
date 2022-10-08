using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Controllers
{
    public class ClubsController : Controller
    {
        private readonly IClubService _clubService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        // GET: ClubsController
        public ClubsController(IClubService clubService, IMapper mapper, IConfiguration config)
        {
            _clubService = clubService;
            _mapper = mapper;
            _config = config;
        }
        // GET: ClubsController
        public async Task<ActionResult> Index()
        {
            var model = new ClubsCollectionViewModel();
            var clubs = await _clubService.GetAll();
            model.Clubs = _mapper.Map<IEnumerable<ClubViewModel>>(clubs);
            return View(model);

            //var rides = await _clubService.GetAll();
            //var model = rides.Select(ride => _mapper.Map<ClubViewModel>(ride));
            //return View(model);
        }

        // GET: ClubsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var club = await _clubService.Get(id);
            if (club != null)
            {
                return View(_mapper.Map<ClubViewModel>(club));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ClubsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClubsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClubViewModel model)
        {
            //model.Club.ApplicationUser = new ApplicationUser { Id = 1 };

            ModelState.Remove("Club.User");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _clubService.Add(model.Club);
            return RedirectToAction(nameof(Details), new { Id = model.Club.Id });
        }

        // GET: ClubsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var club = await _clubService.Get(id);
            if (club != null)
            {
                return View(_mapper.Map<ClubViewModel>(club));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ClubsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ClubViewModel model)
        {
            ModelState.Remove("Club.User");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _clubService.Update(id, _mapper.Map<Club>(model));
                return RedirectToAction(nameof(Details), new { Id = id });
            }
            catch
            {
                return View();
            }
        }


        // GET: ClubsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var club = await _clubService.Get(id);
            if (club != null)
            {
                return View(_mapper.Map<ClubViewModel>(club));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ClubsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ClubViewModel model)
        {
            await _clubService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
