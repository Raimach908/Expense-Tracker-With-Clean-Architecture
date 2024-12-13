using ExpenseTracker.Core.Interfaces;
using System;

namespace ExpenseTracker.Application.UseCases.IncomeUseCases
{
    public class GetMonthlyReportUseCase
    {
        private readonly IIncomeRepository _incomeRepository;

        public GetMonthlyReportUseCase(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public decimal Execute(int userId, DateTime date)
        {
            return _incomeRepository.GetMonthlyReport(userId, date);
        }
    }
}
