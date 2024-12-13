using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.ExpenseUseCases
{
    public class DeleteExpenseUseCase
    {
        private readonly IExpenseRepository _expenseRepository;

        public DeleteExpenseUseCase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public void Execute(int id)
        {
            _expenseRepository.DeleteExpense(id);
        }
    }
}
