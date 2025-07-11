using BusinessObjects;

namespace Services
{
    public interface IUserService
    {
        User GetUserByNameAndPassword(string username, string password);
        bool AddUser(string username, string password, string email, string fullName, DateTime dateOfBirth);
        bool IsUsernameExists(string username);
        bool IsEmailExists(string email);
        List<User> GetAllUsers();
    }
}
