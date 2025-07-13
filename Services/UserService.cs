using BusinessObjects;
using Repositories;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iUserRepository;
        public UserService()
        {
            iUserRepository = new UserRepository();
        }

        public bool AddUser(string username, string password, string email, string fullName, DateTime dateOfBirth)
        {
            return iUserRepository.AddUser(username, password, email, fullName, dateOfBirth);
        }

        public User GetUserByNameAndPassword(string username, string password)
        {
            return iUserRepository.GetUserByNameAndPassword(username, password);
        }

        public bool IsEmailExists(string email)
        {
            return iUserRepository.IsEmailExists(email);
        }

        public bool IsUsernameExists(string username)
        {
            return iUserRepository.IsUsernameExists(username);
        }
        public List<User> GetAllUsers()
        {
            return iUserRepository.GetAllUsers();
        }

        public List<User> GetUserChatWithCoach(List<int> idUser)
        {
            return iUserRepository.GetUserChatWithCoach(idUser);
        }

        public User GetUserById(int id)
        {
            return iUserRepository.GetUserById(id);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
            iUserRepository.UpdateUser(user);
        }

        public List<User> GetUsersForManagement()
        {
            return iUserRepository.GetUsersForManagement();
        }

        public void DeleteUser(int userId)
        {
            iUserRepository.DeleteUser(userId);
        }
    }
}
