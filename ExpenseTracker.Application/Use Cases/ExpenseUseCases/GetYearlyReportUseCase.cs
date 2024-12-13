using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.ExpenseUseCases
{
    public class GetYearlyReportUseCase
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetYearlyReportUseCase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public decimal Execute(int userId, int year)
        {
            return _expenseRepository.GetYearlyReport(userId, year);
        }
    }
}
