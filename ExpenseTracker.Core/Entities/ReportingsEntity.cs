namespace ExpenseTracker.Core.Entities
{
    public class ReportingsEntity
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public DateTime LastIncome { get; set; }
        public DateTime LastExpense { get; set; }
        public decimal TotalBalance {get; set; }
        public decimal MonthlyReport { get; set; }
        public decimal YearlyReport { get; set; }
        public decimal DailyReport { get; set; }
    }

}
