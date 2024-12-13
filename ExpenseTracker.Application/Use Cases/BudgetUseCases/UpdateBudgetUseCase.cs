using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.BudgetUseCases
{
    public class UpdateBudgetUseCase
    {
        private readonly IBudgetRepository _budgetRepository;

        public UpdateBudgetUseCase(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public void Execute(BudgetEntity budget)
        {
            _budgetRepository.UpdateBudget(budget);
        }
    }
}
