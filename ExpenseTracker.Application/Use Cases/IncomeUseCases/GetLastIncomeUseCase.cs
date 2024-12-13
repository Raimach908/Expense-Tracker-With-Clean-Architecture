using ExpenseTracker.Core.Interfaces;
using System;

namespace ExpenseTracker.Application.UseCases.IncomeUseCases
{
    public class GetLastIncomeUseCase
    {
        private readonly IIncomeRepository _incomeRepository;

        public GetLastIncomeUseCase(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public DateTime Execute(int userId)
        {
            return _incomeRepository.GetLastIncome(userId);
        }
    }
}
