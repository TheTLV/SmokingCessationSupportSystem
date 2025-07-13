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
        List<User> GetUserChatWithCoach(List<int> idUser);
        User GetUserById(int id);
        void UpdateUser(User user);
        List<User> GetUsersForManagement();
        void DeleteUser(int userId);
    }
}
