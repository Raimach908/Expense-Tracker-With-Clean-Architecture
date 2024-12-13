using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IUserRepository
    {
        UserEntity GetUserByEmail(string email);
        void AddUser(UserEntity user);
        bool CheckUserExists(string email);
        UserEntity GetUserProfile(int userId);
        public void updateUserProfile(UserEntity user);
       public void deleteProfile(int userId);
    }
}
