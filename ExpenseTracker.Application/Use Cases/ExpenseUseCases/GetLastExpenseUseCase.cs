using ExpenseTracker.Core.Interfaces;
using System;

namespace ExpenseTracker.Application.UseCases.ExpenseUseCases
{
    public class GetLastExpenseUseCase
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetLastExpenseUseCase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public DateTime Execute(int userId)
        {
            return _expenseRepository.GetLastExpense(userId);
        }
    }
}
