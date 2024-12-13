using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.ExpenseUseCases
{
    public class GetTotalExpenseUseCase
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetTotalExpenseUseCase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public decimal Execute(int userId)
        {
            return _expenseRepository.GetTotalExpense(userId);
        }
    }
}
