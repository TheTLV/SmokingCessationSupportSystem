using BusinessObjects;

namespace DataAccessLayout
{
    public static class UserDAO
    {
        public static User GetUserByNameAndPassword(string username, string password)
        {
            using var db = new AppDbContext();
            return db.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
        }

        public static bool AddUser(string username, string password, string Email, string FullName, DateTime DateOfBirth)
        {
            using var db = new AppDbContext();
            User user = new User
            {
                Username = username,
                PasswordHash = password, // In a real application, use a secure hash function
                Email = Email,
                FullName = FullName,
                DateOfBirth = DateOfBirth,
                Role = "User" // Default role
            };

            db.Users.Add(user);
            db.SaveChanges();
            return (user != null);
        }

        public static bool IsUsernameExists(string username)
        {
            using var db = new AppDbContext();
            return db.Users.Any(u => u.Username == username);
        }

        public static bool IsEmailExists(string email)
        {
            using var db = new AppDbContext();
            return db.Users.Any(u => u.Email == email);
        }

        public static List<User> GetAllUsers()
        {
            using var db = new AppDbContext();
            return db.Users.ToList();
        }

        public static List<User> GetUserChatWithCoach(List<int> idUser)
        {
            using var db = new AppDbContext();
            return db.Users
                .Where(u => idUser.Contains(u.Id))
                .ToList();
        }

        public static User GetUserById(int id)
        {
            using var db = new AppDbContext();
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public static void UpdateUser(User user)
        {
            using var db = new AppDbContext();
            var existingUser = db.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.FullName = user.FullName;
                existingUser.DateOfBirth = user.DateOfBirth;
                db.SaveChanges();
            }
        }

        public static List<User> GetUsersForManagement()
        {
            using var db = new AppDbContext();
            return db.Users
                .Where(u => u.Role == "User")
                .ToList();
        }

        public static void DeleteUser(int userId)
        {
            using var db = new AppDbContext();
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.Role = "Deleted";
                db.SaveChanges();
            }
        }
    }
}
