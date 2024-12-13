using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.ExpenseUseCases
{
    public class GetExpenseByIdUseCase
    {
        private readonly IExpenseRepository _expenseRepository;

        public GetExpenseByIdUseCase(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public ExpenseEntity Execute(int id)
        {
            return _expenseRepository.GetExpenseById(id);
        }
    }
}
