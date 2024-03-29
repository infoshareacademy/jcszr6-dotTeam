﻿namespace PlanAndRide.BusinessLogic
{
    public class RideService:IRideService
    {
        private readonly IRepository<Ride> _repository;
        public RideService() { }
        public RideService(IRepository<Ride> repository)
        {
            _repository = repository;
        }
        
        public async Task Add(Ride ride)
        {
            await _repository.Add(ride);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<Ride> Get(int id)
        {
            {
                try
                {
                    return await _repository.Get(id);
                }
                catch
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Ride>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Update(int id, Ride ride)
        {
            try
            {
                await _repository.Update(id, ride);
            }
            catch
            {
                throw;
            };
        }
        public async Task<IEnumerable<Ride>> FindByName(string name)
        {
            var rides = await _repository.GetAll();
            return rides.Where(r => r.Name.ToLower().Contains(name.Trim().ToLower()));
        }
    }
}
