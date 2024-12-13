using ExpenseTracker.Core.Interfaces;
using System;

namespace ExpenseTracker.Application.UseCases.ExpenseUseCases
{
    public class GetMonthlyReportUseCase
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetMonthlyReportUseCase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public decimal Execute(int userId, DateTime date)
        {
            return _expenseRepository.GetMonthlyReport(userId, date);
        }
    }
}
