using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.BudgetUseCases
{
    public class AddBudgetUseCase
    {
        private readonly IBudgetRepository _budgetRepository;

        public AddBudgetUseCase(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public void Execute(BudgetEntity budget)
        {
            _budgetRepository.AddBudget(budget);
        }
    }
}
