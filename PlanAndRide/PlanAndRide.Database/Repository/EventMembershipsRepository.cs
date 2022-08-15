using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic;
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
                             Login="Marek",
                             User=new User
                             {
                                 Password="1234",
                                 Login="Marek"
                             },
                             DateOfJoin = DateTime.Now,
                             Comment = "Bedzie dobrze",
                         },
                         new EventMemberships
                         {
                             Id=2,
                             Login="Kociak",
                             User=new User
                             {
                                 Login="Kociak",
                                 Password="1234"
                             },
                             DateOfJoin= DateTime.Now,
                             Comment="Jazda Panowie",
                         },
                         new EventMemberships
                         {
                             Id=3,
                             Login="Twardziel",
                             User=new User
                             {
                                 Login="Twardziel",
                                 Password="123"
                             },
                             DateOfJoin= DateTime.Now,
                             Comment="Carpet Dijen",
                         },
                         new EventMemberships
                         {
                             Id=4,
                             Login="Maczo",
                             User=new User
                             {
                                 Login="Maczo",
                                 Password ="123"
                             },
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
            existingEventMemberships.User.Login = eventMemberships.User.Login;
            existingEventMemberships.DateOfJoin = eventMemberships.DateOfJoin;
            existingEventMemberships.Comment = eventMemberships.Comment;
        }
        public void Delete (int id)
        {
            _ = EventMemberships.Remove(Get(id));
        }
        private static List<User> user=new List<User>();
    }
}

    
