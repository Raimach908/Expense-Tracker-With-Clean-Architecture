using System.Collections.Generic;
using ExpenseTracker.Core.Entities;
namespace ExpenseTracker.Core.Interfaces
{
    public interface IExpenseRepository
    {
        IEnumerable<ExpenseEntity> GetAllExpenses(int userId);
        ExpenseEntity GetExpenseById(int id);
        void AddExpense(ExpenseEntity expense);
        void UpdateExpense(ExpenseEntity expense);
        void DeleteExpense(int id);
        public bool IsBalanceSufficient(int userId, decimal expenseAmount);
        decimal GetTotalExpense(int userId);
        DateTime GetLastExpense(int userId);
        decimal GetMonthlyReport(int userId, DateTime date);
        decimal GetYearlyReport(int userId, int year);
        decimal GetDailyReport(int userId, DateTime date);
    }
}
