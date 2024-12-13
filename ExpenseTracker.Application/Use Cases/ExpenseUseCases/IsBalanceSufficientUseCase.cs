using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.ExpenseUseCases
{
    public class IsBalanceSufficientUseCase
    {
        private readonly IExpenseRepository _expenseRepository;

        public IsBalanceSufficientUseCase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public bool Execute(int userId, decimal expenseAmount)
        {
            return _expenseRepository.IsBalanceSufficient(userId, expenseAmount);
        }
    }
}
