using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.UserUseCases
{
    public class CheckUserExistsUseCase
    {
        private readonly IUserRepository _userRepository;

        public CheckUserExistsUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Execute(string email)
        {
            return _userRepository.CheckUserExists(email);
        }
    }
}
