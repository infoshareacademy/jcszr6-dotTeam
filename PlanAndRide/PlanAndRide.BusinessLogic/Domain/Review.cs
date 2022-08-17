namespace PlanAndRide.BusinessLogic
{
    public class Review
    {
        public int Id { get; set; }
        public int ReferenceId { get; set; }
        public ReviewType Type { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public DateTime Date { get;  set; }
        public string Description { get; set; }
    }
}