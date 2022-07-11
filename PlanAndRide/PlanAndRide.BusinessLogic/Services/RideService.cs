﻿namespace PlanAndRide.BusinessLogic
{
    public class RideService
    {
        private readonly IRepository<Ride> _repository;

        public RideService(IRepository<Ride> repository)
        {
            _repository = repository;
        }
        public IEnumerable<Ride> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
