namespace PlanAndRide.BusinessLogic
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public int Score { get; set; }
        public DateTime Date { get;  set; }
        public string? Description { get; set; }
    }
}