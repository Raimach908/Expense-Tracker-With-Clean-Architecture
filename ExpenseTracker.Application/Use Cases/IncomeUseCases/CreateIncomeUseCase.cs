using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.IncomeUseCases
{
    public class CreateIncomeUseCase
    {
        private readonly IIncomeRepository _incomeRepository;

        public CreateIncomeUseCase(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public void Execute(IncomeEntity income)
        {
            _incomeRepository.AddIncome(income);
        }
    }
}
