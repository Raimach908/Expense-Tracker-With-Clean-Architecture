using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.BudgetUseCases
{
    public class GetBudgetByIdUseCase
    {
        private readonly IBudgetRepository _budgetRepository;

        public GetBudgetByIdUseCase(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public BudgetEntity Execute(int id)
        {
            return _budgetRepository.GetBudgetById(id);
        }
    }
}
