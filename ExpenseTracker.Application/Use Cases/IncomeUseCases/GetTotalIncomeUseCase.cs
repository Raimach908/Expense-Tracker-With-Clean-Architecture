using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.IncomeUseCases
{
    public class GetTotalIncomeUseCase
    {
        private readonly IIncomeRepository _incomeRepository;

        public GetTotalIncomeUseCase(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public decimal Execute(int userId)
        {
            return _incomeRepository.GetTotalIncome(userId);
        }
    }
}
