using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.UserUseCases
{
    public class AddUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public AddUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(UserEntity user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("Email and password are required.");

            if (_userRepository.CheckUserExists(user.Email))
                throw new InvalidOperationException("A user with this email already exists.");

            _userRepository.AddUser(user);
        }
    }
}
