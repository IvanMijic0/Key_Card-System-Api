using Keycard_System_API.Models;

namespace Key_Card_System_Api.Repositories.UserRepository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User? GetUserById(int id);
        void CreateUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(int id);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByUsername(string username);
    }
}
