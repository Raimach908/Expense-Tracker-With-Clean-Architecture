using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.UserUseCases
{
    public class DeleteUserProfileUseCase
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserProfileUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.");

            _userRepository.deleteProfile(userId);
        }
    }
}
