﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public interface IRideRepository : IRepository<Ride>
    {
        Task<IEnumerable<Ride>> GetByUser(string id);
        Task<IEnumerable<Ride>> GetPublic();
        Task AddRideMember(Ride ride, string userId);
        Task RemoveRideMember(Ride ride, string userId);
    }
}
