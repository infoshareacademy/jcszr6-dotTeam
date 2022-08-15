using PlanAndRide.BusinessLogic;


namespace PlanAndRide.BusinessLogic
{
    public class EventMembershipsService: IEventMembershipsService
    {
        private readonly IRepository<EventMemberships> _repository;
        public EventMembershipsService() { }
        public EventMembershipsService(IRepository<EventMemberships> repository) 
        { 
            _repository = repository; 
        }
        public void Add(EventMemberships eventMemberships)
        {
            _repository.Add(eventMemberships);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public EventMemberships Get(int id)
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

        public IEnumerable<EventMemberships> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(int id, EventMemberships eventMemberships)
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
        //public IEnumerable<EventMemberships> FindByName(int idEventMemberships)
        //{
          //  var idEventMemberships = new List<EventMemberships>;
            //return _repository.GetAll().Where(r => r.Id.ToLower().Contains(name.Trim().ToLower()));
        //}
    }

}

