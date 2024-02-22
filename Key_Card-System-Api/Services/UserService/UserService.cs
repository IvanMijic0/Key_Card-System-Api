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

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task CreateUserAsync(User user)
        {
            await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<bool> DeactivateUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            user.IsActive = false;
            await _userRepository.UpdateUserAsync(user);

            return true;
        }

        public async Task<User?> AuthenticateByUsernameAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user != null && PasswordHash.VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }

        public async Task UpdateUsersKeyCardAcessLevelAsync(int user_id, string response, string access_level)
        {
            await _userRepository.UpdateUsersKeyCardAcessLevelAsync(user_id, response, access_level);
        }

        public async Task<User?> AuthenticateByEmailAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user != null && PasswordHash.VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }
        public async Task<User?> RegisterAsync(User user, string password)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
            if (existingUser != null)
                return null;

            user.PasswordHash = PasswordHash.HashPassword(password);

            user.Keycard.PreviousAccessLevel = user.Keycard.AccessLevel;

            await _userRepository.CreateUserAsync(user);

            return user;
        }

        public async Task<List<User>> SearchUsersByUsernameAsync(string searchTerm)
        {
            return await _userRepository.SearchUsersByUsernameAsync(searchTerm);
        }

        public async Task<List<User>> SearchUsersByKeyIdAsync(string searchTerm)
        {
            return await _userRepository.SearchUsersByKeyIdAsync(searchTerm);
        }

        public async Task UpdateUsersKeyCardAcessLevelAsync(int user_id, string access_level)
        {
            await _userRepository.UpdateUsersKeyCardAcessLevelAsync(user_id, access_level);
        }
    }
}
