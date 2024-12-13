namespace ExpenseTracker.Core.Entities
{
    public class IncomeEntity
    {
        public int Id { get; set; }
        public string category { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
