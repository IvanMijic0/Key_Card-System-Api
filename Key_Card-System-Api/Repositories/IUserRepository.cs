using Keycard_System_API.Models;

namespace Keycard_System_API.Repositories
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
