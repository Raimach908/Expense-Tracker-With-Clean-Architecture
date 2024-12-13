namespace ExpenseTracker.Core.Entities
{
    public class BudgetEntity
    {
        public int Id { get; set; }
        public string category { get; set; } = string.Empty;
        public decimal Limit { get; set; }
        public decimal Spent { get; set; }
        public int UserId { get; set; } // Foreign Key to User
    }
}
