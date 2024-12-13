using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.UserUseCases
{
    public class GetUserProfileUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserProfileUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserEntity Execute(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than zero.");

            var userProfile = _userRepository.GetUserProfile(userId);
            if (userProfile == null)
                throw new InvalidOperationException("User profile not found.");

            return userProfile;
        }
    }
}
