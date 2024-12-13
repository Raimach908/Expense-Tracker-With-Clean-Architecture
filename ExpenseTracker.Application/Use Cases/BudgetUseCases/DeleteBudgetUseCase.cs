using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.BudgetUseCases
{
    public class DeleteBudgetUseCase
    {
        private readonly IBudgetRepository _budgetRepository;

        public DeleteBudgetUseCase(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public void Execute(int id)
        {
            _budgetRepository.DeleteBudget(id);
        }
    }
}
