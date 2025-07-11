using BusinessObjects;

namespace Repositories
{
    public interface IUserRepository
    {
        bool IsUsernameExists(string username);
        bool IsEmailExists(string email);
        User GetUserByNameAndPassword(string username, string password);
        bool AddUser(string username, string password, string email, string fullName, DateTime dateOfBirth);
        List<User> GetAllUsers();
    }
}
