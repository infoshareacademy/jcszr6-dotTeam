using AutoMapper;
using PlanAndRide.BusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PlanAndRide.BusinessLogic
{
    public class RideService : IRideService
    {
        private readonly IRideRepository _repository;
        private readonly IMapper _mapper;
        public RideService() { }
        public RideService(IRideRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Add(EventDto dto)
        {
            var ride = _mapper.Map<Ride>(dto);
            await _repository.Add(ride);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<EventDto> Get(int id, string userId)
        {
            {
                try
                {

                    var ride = await _repository.Get(id);
                    var model = _mapper.Map<EventDto>(ride);
                    model.StatusRide = GetRideStatus(model);
                    model.IsViewerJoined = ride.RideMembers.Any(u => u.Id == userId);
                    return model;
                }
                catch
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<EventDto>> GetAll()
        {
            var rides = await _repository.GetAll();
            var model = _mapper.Map<IEnumerable<EventDto>>(rides);
            foreach (var ride in model)
            {
                ride.StatusRide = GetRideStatus(ride);
            }
            return model;
        }
        public async Task<IEnumerable<EventDto>> GetByUser(string id)
        {
            var rides = await _repository.GetByUser(id);
            var model = _mapper.Map<IEnumerable<EventDto>>(rides);
            foreach (var ride in model)
            {
                ride.StatusRide = GetRideStatus(ride);
            }
            return model;
        }
        public async Task<IEnumerable<EventDto>> GetPublic()
        {
            var rides = await _repository.GetPublic();
            var model = _mapper.Map<IEnumerable<EventDto>>(rides);
            foreach (var ride in model)
            {
                ride.StatusRide = GetRideStatus(ride);
            }
            return model;
        }

        public async Task Update(int id, EventDto dto)
        {
            try
            {
                var ride = _mapper.Map<Ride>(dto);
                await _repository.Update(id, ride);
            }
            catch
            {
                throw;
            };
        }
        //public async Task<IEnumerable<EventDto>> FindByName(string name)
        //{
        //    var rides = await _repository.GetAll();
        //    return rides.Where(r => r.Name.ToLower().Contains(name.Trim().ToLower()));
        //}
        public string GetRideStatus(EventDto ride)
        {
            var date2 = DateTime.Now.Date;
            var date1 = ride.Date.Date;

            var compareDate = DateTime.Compare(date1, date2);
            if (compareDate > 0)
            {
                var status = StatusList.Comming;
                var nameStatus = status.GetType().GetMember(status.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();
                return nameStatus;
            }
            if (compareDate == 0)
            {
                var status = StatusList.Right_Now;
                var nameStatus = status.GetType().GetMember(status.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();
                return nameStatus;
            }
            if (compareDate < 0)
            {
                var status = StatusList.Completed;
                var nameStatus = status.GetType().GetMember(status.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();
                return nameStatus;
            }
            else
            {
                var status = StatusList.Unknown;
                var nameStatus = status.GetType().GetMember(status.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();
                return nameStatus;
            }

        }
        public async Task AddRideMember(int rideId, string userId)
        {
            var ride = await _repository.Get(rideId);
            if (ride == null || ride.RideMembers.Any(u => u.Id == userId))
            {
                return;
            }
            await _repository.AddRideMember(ride, userId);
        }
        public async Task RemoveRideMember(int rideId, string userId)
        {
            var ride = await _repository.Get(rideId);
            if (ride == null || !ride.RideMembers.Any(u => u.Id == userId))
            {
                return;
            }
            await _repository.RemoveRideMember(ride, userId);
        }
    }
}