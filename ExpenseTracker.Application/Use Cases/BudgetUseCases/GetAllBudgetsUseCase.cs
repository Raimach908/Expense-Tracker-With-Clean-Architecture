using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;
using System.Collections.Generic;

namespace ExpenseTracker.Application.UseCases.BudgetUseCases
{
    public class GetAllBudgetsUseCase
    {
        private readonly IBudgetRepository _budgetRepository;

        public GetAllBudgetsUseCase(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public IEnumerable<BudgetEntity> Execute(int userId)
        {
            return _budgetRepository.GetAllBudgets(userId);
        }
    }
}
