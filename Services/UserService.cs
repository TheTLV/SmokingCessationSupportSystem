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
    }
}
