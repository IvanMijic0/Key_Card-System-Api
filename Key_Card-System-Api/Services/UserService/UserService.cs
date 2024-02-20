using Key_Card_System_Api.Repositories.UserRepository;
using Keycard_System_API.Models;
using Keycard_System_API.Utils;


namespace Key_Card_System_Api.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User? GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public void CreateUser(User user)
        {
            _userRepository.CreateUser(user);
        }

        public User UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);

            return user;
        }

        public bool DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public bool DeactivateUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return false;
            }

            user.IsActive = false;
            _userRepository.UpdateUser(user);

            return true;
        }

        public async Task<User?> AuthenticateByUsername(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user != null && PasswordHash.VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }

        public async Task<User?> AuthenticateByEmail(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user != null && PasswordHash.VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }

        public async Task<User?> Register(User user, string password)
        {
            var existingUser = await _userRepository.GetUserByUsername(user.Username);
            if (existingUser != null)
                return null;

            user.PasswordHash = PasswordHash.HashPassword(password);

            _userRepository.CreateUser(user);

            return user;
        }
    }
}
