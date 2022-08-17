namespace PlanAndRide.BusinessLogic
{
    public class Review
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int RouteId { get; set; }
        public virtual Route Route { get; set; }
        public int Score { get; set; }
        public virtual DateTime Date { get;  set; }
        public string? Description { get; set; }
    }
}