using PlanAndRide.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic.Services
{
    internal class EventMembershipsService: IEventMemberships
    {
        private readonly IRepository<EventMembershipsService> _repository;
        public EventMembershipsService() { }
        public EventMembershipsService(IRepository<EventMembershipsService> repository) 
        { _repository = repository; }
        public void Add(EventMembershipsService eventMemberships)
        {
            _repository.Add(eventMemberships);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public EventMembershipsService Get(int id)
        {
            {
                try
                {
                    return _repository.Get(id);
                }
                catch
                {
                    throw;
                }
            }
        }

        public IEnumerable<EventMembershipsService> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(int id, EventMembershipsService eventMemberships)
        {
            try
            {
                _repository.Update(id, eventMemberships);
            }
            catch
            {
                throw;
            };
        }
        //public IEnumerable<EventMemberships> FindByName(string nickname)
        //{
        //    var nicknames = new List<User>; 
        //        return _repository.GetAll().Where(r => r..ToLower().Contains(name.Trim().ToLower()));
        //}
    }

}

