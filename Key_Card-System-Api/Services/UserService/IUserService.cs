using Keycard_System_API.Models;

namespace Key_Card_System_Api.Services.UserService
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User? GetUserById(int id);
        void CreateUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(int id);
        bool DeactivateUser(int id);
        Task<User?> AuthenticateByUsername(string username, string password);
        Task<User?> AuthenticateByEmail(string email, string password);
        Task<User?> Register(User user, string password);
    }
}
