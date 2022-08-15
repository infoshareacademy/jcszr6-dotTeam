using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanAndRide.BusinessLogic;
using PlanAndRide.Web.Models;

namespace PlanAndRide.Web.Controllers
{
    public class EventMembershipsController : Controller
    {
        private readonly IEventMembershipsService _eventMembershipsService;
        //private readonly IMapper _mapper;
        public EventMembershipsController(IEventMembershipsService eventMembershipsService, IMapper mapper)
        {
            _eventMembershipsService = eventMembershipsService;
            //_mapper = mapper;
        }
        
        // GET: EventMembershipsController
        public ActionResult Index()
        {

            // var model= _eventMemberships.GetAll().Select(eventMemberships=>_mapper.Map<EventMembershipsViewModel>(eventMemberships));
            var model = new EventMembershipsCollectionViewModel();
            model.EventMemberships = _eventMembershipsService.GetAll().Select(e => new EventMembershipsViewModel());
            return View(model);
        }

        // GET: EventMembershipsController/Details/5
        public ActionResult Details(int id)
        {
            var eventMemberships= _eventMembershipsService.Get(id);
            if(eventMemberships!=null)
            {
                return View(new EventMembershipsViewModel());
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: EventMembershipsController/Create
        public ActionResult Create()
        {
            //var user = _userService.GetAll();
            //var model = new EventMembershipsViewModel() { User = users };
            //return View(model);
            return View();
        }

        // POST: EventMembershipsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventMembershipsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //model. = _userService.GetAll();
                return View(model);
            }

           // var ride = _mapper.Map<EventMemberships>(model);
            //if (int.TryParse(model.UserId, out int id))
            //    eventMemberships.User = _userService.Get(id);
            //else
            //    eventMemberships.User = null;
            //_eventMemberships.Add(eventMemberships);
            _eventMembershipsService.Add(model.EventMemberships);
            return RedirectToAction(nameof(Index));
        }

        // GET: EventMembershipsController/Edit/5
        public ActionResult Edit(int id)
        {
            var eventMemberships=_eventMembershipsService.Get(id);
            if(eventMemberships!=null)
            {
                return View(new EventMembershipsViewModel());
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: EventMembershipsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventMembershipsViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                _eventMembershipsService.Update(id, model.EventMemberships);
                return RedirectToAction(nameof(Details), new {id=id});
            }
            catch
            {
                return View();
            }
        }

        // GET: EventMembershipsController/Delete/5
        public ActionResult Delete(int id)
        {
            var eventMemberships= _eventMembershipsService.Get(id);
            if(eventMemberships!= null)
            {
                return View(new EventMembershipsViewModel());
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: EventMembershipsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EventMembershipsViewModel model)
        {
            _eventMembershipsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
