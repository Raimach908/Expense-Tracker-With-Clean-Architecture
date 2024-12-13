using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.ExpenseUseCases
{
    public class GetAllExpensesUseCase
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetAllExpensesUseCase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public IEnumerable<ExpenseEntity> Execute(int userId)
        {
            return _expenseRepository.GetAllExpenses(userId);
        }
    }
}
