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

        public void DeleteUser(int userId)
        {
            UserDAO.DeleteUser(userId);
        }

        public List<User> GetAllUsers()
        {
            return UserDAO.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return UserDAO.GetUserById(id);
        }

        public User GetUserByNameAndPassword(string username, string password)
        {
            return UserDAO.GetUserByNameAndPassword(username, password);
        }

        public List<User> GetUserChatWithCoach(List<int> idUser)
        {
            return UserDAO.GetUserChatWithCoach(idUser);
        }

        public List<User> GetUsersForManagement()
        {
            return UserDAO.GetUsersForManagement();
        }

        public bool IsEmailExists(string email)
        {
            return UserDAO.IsEmailExists(email);
        }

        public bool IsUsernameExists(string username)
        {
            return UserDAO.IsUsernameExists(username);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
            UserDAO.UpdateUser(user);
        }
    }
}
