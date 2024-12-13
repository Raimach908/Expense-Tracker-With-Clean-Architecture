using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.IncomeUseCases
{
    public class GetIncomeByIdUseCase
    {
        private readonly IIncomeRepository _incomeRepository;

        public GetIncomeByIdUseCase(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public IncomeEntity Execute(int id)
        {
            return _incomeRepository.GetIncomeById(id);
        }
    }
}
