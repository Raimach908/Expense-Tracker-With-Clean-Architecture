using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.IncomeUseCases
{
    public class UpdateIncomeUseCase
    {
        private readonly IIncomeRepository _incomeRepository;

        public UpdateIncomeUseCase(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public void Execute(IncomeEntity income)
        {
            _incomeRepository.UpdateIncome(income);
        }
    }
}
