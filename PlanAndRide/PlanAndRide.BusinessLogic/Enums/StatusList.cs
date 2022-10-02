using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic.Enums
{
    public enum StatusList
    {
        [Description("Unknown")] Unknown,
        [Description("Is coming")] Comming,
        [Description("Right now")] Right_Now,
        [Description("Completed")] Completed,
        [Description("Archived")] Archived
    }
}  
