using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.UserUseCases
{
    public class LoginUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public LoginUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserEntity Execute(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            // If user not found, return null
            if (user == null)
                return null;

            // Validate password
            if (user.Password != password)
                throw new UnauthorizedAccessException("Invalid password.");

            return user;
        }
    }
}
