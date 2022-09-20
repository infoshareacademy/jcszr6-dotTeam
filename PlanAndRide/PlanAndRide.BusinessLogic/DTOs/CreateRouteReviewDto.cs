using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class CreateRouteReviewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RouteId { get; set; }
        [Required]
        [Range(1,5)]
        public int Score { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
