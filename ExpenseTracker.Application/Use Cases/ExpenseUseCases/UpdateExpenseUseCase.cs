using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.ExpenseUseCases
{
    public class UpdateExpenseUseCase
    {
        private readonly IExpenseRepository _expenseRepository;

        public UpdateExpenseUseCase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public void Execute(ExpenseEntity expense)
        {
            _expenseRepository.UpdateExpense(expense);
        }
    }
}
