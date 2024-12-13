using ExpenseTracker.Core.Entities;
namespace ExpenseTracker.Core.Interfaces
{
    public interface IBudgetRepository
    {
        IEnumerable<BudgetEntity> GetAllBudgets(int userId);
        BudgetEntity GetBudgetById(int id);
        void AddBudget(BudgetEntity budget);
        void UpdateBudget(BudgetEntity budget);
        void DeleteBudget(int id);
    }
}
