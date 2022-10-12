using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic.Enums
{
    public enum StatusList
    {
        [Display(Name = "Unknown")] Unknown,
        [Display(Name = "Is coming")] Comming,
        [Display(Name = "Right now")] Right_Now,
        [Display(Name = "Completed")] Completed,
        [Display(Name = "Archived")] Archived
    }
}
