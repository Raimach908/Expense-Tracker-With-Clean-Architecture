using ExpenseTracker.Core.Interfaces;
using System;

namespace ExpenseTracker.Application.UseCases.ExpenseUseCases
{
    public class GetDailyReportUseCase
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetDailyReportUseCase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public decimal Execute(int userId, DateTime date)
        {
            return _expenseRepository.GetDailyReport(userId, date);
        }
    }
}
