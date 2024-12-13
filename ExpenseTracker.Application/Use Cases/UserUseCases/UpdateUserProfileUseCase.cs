using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.UserUseCases
{
    public class UpdateUserProfileUseCase
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserProfileUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(UserEntity user)
        {
            if (user.UserId <= 0)
                throw new ArgumentException("Invalid user ID.");

            _userRepository.updateUserProfile(user);
        }
    }
}
