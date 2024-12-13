using System.Collections.Generic;
using ExpenseTracker.Core.Entities;
namespace ExpenseTracker.Core.Interfaces
{
    public interface IIncomeRepository
    {
        IEnumerable<IncomeEntity> GetAllIncome(int userId);
        IncomeEntity GetIncomeById(int id);
        void AddIncome(IncomeEntity income);
        void UpdateIncome(IncomeEntity income);
        void DeleteIncome(int id);
        decimal GetTotalIncome(int userId);
        DateTime GetLastIncome(int userId);
        decimal GetMonthlyReport(int userId, DateTime date);
        decimal GetYearlyReport(int userId, int year);
        decimal GetDailyReport(int userId, DateTime date);
    }
}
