using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.IncomeUseCases
{
    public class DeleteIncomeUseCase
    {
        private readonly IIncomeRepository _incomeRepository;

        public DeleteIncomeUseCase(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public void Execute(int id)
        {
            _incomeRepository.DeleteIncome(id);
        }
    }
}
