using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.IncomeUseCases
{
    public class GetYearlyReportUseCase
    {
        private readonly IIncomeRepository _incomeRepository;

        public GetYearlyReportUseCase(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public decimal Execute(int userId, int year)
        {
            return _incomeRepository.GetYearlyReport(userId, year);
        }
    }
}
