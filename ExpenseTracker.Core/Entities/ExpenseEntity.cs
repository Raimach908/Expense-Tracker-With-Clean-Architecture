namespace ExpenseTracker.Core.Entities
{
    public class ExpenseEntity
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
