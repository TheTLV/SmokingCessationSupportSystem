using BusinessObjects;
using DataAccessLayout;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public bool AddUser(string username, string password, string email, string fullName, DateTime dateOfBirth)
        {
            return UserDAO.AddUser(username, password, email, fullName, dateOfBirth);
        }

        public List<User> GetAllUsers()
        {
            return UserDAO.GetAllUsers();
        }

        public User GetUserByNameAndPassword(string username, string password)
        {
            return UserDAO.GetUserByNameAndPassword(username, password);
        }

        public List<User> GetUserChatWithCoach(List<int> idUser)
        {
            return UserDAO.GetUserChatWithCoach(idUser);
        }

        public bool IsEmailExists(string email)
        {
            return UserDAO.IsEmailExists(email);
        }

        public bool IsUsernameExists(string username)
        {
            return UserDAO.IsUsernameExists(username);
        }
    }
}
