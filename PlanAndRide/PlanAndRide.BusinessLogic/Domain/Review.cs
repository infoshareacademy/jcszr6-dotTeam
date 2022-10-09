namespace PlanAndRide.BusinessLogic
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public int RouteId { get; set; }
        public virtual Route Route { get; set; }
        public int Score { get; set; }
        public virtual DateTime Date { get;  set; }
        public string? Description { get; set; }
    }
}