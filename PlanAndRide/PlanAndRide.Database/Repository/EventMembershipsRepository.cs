using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Domain;
using PlanAndRide.BusinessLogic.Exceptions;

namespace PlanAndRide.Database.Repository
{
    public class EventMembershipsRepository : IRepository<EventMemberships>
    {


        private static List<EventMemberships>
                 EventMemberships = new List<EventMemberships>
                 {

                         new EventMemberships
                         {
                             Id = 1,
                             Nickname ="Edwar",
                             DateOfJoin = DateTime.Now,
                             Comment = "Bedzie dobrze",
                         },
                         new EventMemberships
                         {
                             Id=2,
                             Nickname="Ksawery",
                             DateOfJoin= DateTime.Now,
                             Comment="Jazda Panowie",
                         },
                         new EventMemberships
                         {
                             Id=3,
                             Nickname="Twardziel",
                             DateOfJoin= DateTime.Now,
                             Comment="Carpet Dijen",
                         },
                         new EventMemberships
                         {
                             Id=4,
                             Nickname="Maczo",
                             DateOfJoin= DateTime.Now,
                             Comment="Lecimy",
                         }
                     };



        public EventMembershipsRepository()
        {

        }
        public EventMemberships? Get(int id)
        {
            try
            {
                return EventMemberships.SingleOrDefault(x => x.Id == id);
            }
            catch
            {
                throw new InvalidOperationException($"Unique key violaton: EventMemberships Id:{id}");
            }
        }
        public IEnumerable<EventMemberships> GetAll()
        {
            return EventMemberships;
        }
        public void Add(EventMemberships eventMemberships)
        {
            if(EventMemberships.Count > 0)
            {
                eventMemberships.Id = EventMemberships.Max(x => x.Id) + 1;
            }
            else
            {
                eventMemberships.Id = 1;
            }
            EventMemberships.Add(eventMemberships);
        }
        public void Update(int id, EventMemberships eventMemberships)
        {
            var existingEventMemberships=Get(id);
            if(existingEventMemberships == null)
            {
                throw new RecordNotFoundException($"EventMemberships ID: {id} not found in repository");
            }
            existingEventMemberships.Nickname = eventMemberships.Nickname;
            existingEventMemberships.DateOfJoin = eventMemberships.DateOfJoin;
            existingEventMemberships.Comment = eventMemberships.Comment;
        }
        public void Delete (int id)
        {
            _ = EventMemberships.Remove(Get(id));
        }
    }
}

    
