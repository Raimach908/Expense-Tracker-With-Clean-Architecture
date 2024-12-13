using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.IncomeUseCases
{
    public class GetAllIncomeUseCase
    {
        private readonly IIncomeRepository _incomeRepository;

        public GetAllIncomeUseCase(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public IEnumerable<IncomeEntity> Execute(int userId)
        {
            return _incomeRepository.GetAllIncome(userId);
        }
    }
}
