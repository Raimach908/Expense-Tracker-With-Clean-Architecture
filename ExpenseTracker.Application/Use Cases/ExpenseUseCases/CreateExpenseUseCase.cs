using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.ExpenseUseCases
{
    public class CreateExpenseUseCase
    {
        private readonly IExpenseRepository _expenseRepository;

        public CreateExpenseUseCase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public void Execute(ExpenseEntity expense)
        {
            if (!_expenseRepository.IsBalanceSufficient(expense.UserId, expense.Amount))
            {
                throw new InvalidOperationException("Insufficient balance to add this expense.");
            }

            _expenseRepository.AddExpense(expense);
        }
    }
}
