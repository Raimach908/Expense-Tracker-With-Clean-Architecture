using ExpenseTracker.Core.Interfaces;
using System;

namespace ExpenseTracker.Application.UseCases.IncomeUseCases
{
    public class GetDailyReportUseCase
    {
        private readonly IIncomeRepository _incomeRepository;

        public GetDailyReportUseCase(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public decimal Execute(int userId, DateTime date)
        {
            return _incomeRepository.GetDailyReport(userId, date);
        }
    }
}
