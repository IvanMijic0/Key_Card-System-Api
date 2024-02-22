using Key_Card_System_Api.Models;
using Key_Card_System_Api.Repositories.KeycardRepository;
using Key_Card_System_Api.Repositories.UserRepository;
using Keycard_System_API.Models;

namespace Key_Card_System_Api.Services.KeycardService
{
    public class KeycardService : IKeycardService
    {
        private readonly IKeycardRepository _keycardRepository;
        private readonly IUserRepository _userRepository;

        public KeycardService(IKeycardRepository keycardRepository, IUserRepository userRepository)
        {
            _keycardRepository = keycardRepository ?? throw new ArgumentNullException(nameof(keycardRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<List<Keycard>> GetAllKeycardsAsync()
        {
            return await _keycardRepository.GetAllKeycardsAsync();
        }

        public async Task<Keycard?> GetKeycardByIdAsync(int id)
        {
            return await _keycardRepository.GetKeycardByIdAsync(id);
        }

        public async Task CreateKeycardAsync(Keycard keycard)
        {
            ArgumentNullException.ThrowIfNull(keycard);
            await _keycardRepository.CreateKeycardAsync(keycard);
        }

        public async Task<Keycard> UpdateKeycardAsync(Keycard keycard)
        {
            ArgumentNullException.ThrowIfNull(keycard);
            return await _keycardRepository.UpdateKeycardAsync(keycard);

        }

        public async Task<User> ReplaceKeycardAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId) ?? throw new ArgumentException("User does not exist.");

            var oldKeycard = user.Keycard ?? throw new ArgumentException("Old keycard does not exist for this user.");

            var newKeycard = new Keycard
            {
                AccessLevel = oldKeycard.AccessLevel,
                Key_Id = oldKeycard.Key_Id,
                PreviousAccessLevel = oldKeycard.PreviousAccessLevel,
                IsActive = true
            };
            await _keycardRepository.CreateKeycardAsync(newKeycard);

            user.Keycard = newKeycard;
            await _userRepository.UpdateUserAsync(user);

            await _keycardRepository.DeleteKeycardAsync(oldKeycard.Id);

            return user;
        }

        public async Task<bool> DeactivateKeycardAsync(int id)
        {
            var keycard = await _keycardRepository.GetKeycardByIdAsync(id);
            if (keycard == null)
            {
                return false;
            }

            keycard.IsActive = false;
            await _keycardRepository.UpdateKeycardAsync(keycard);
            return true;
        }

        public async Task<bool> DeleteKeycardAsync(int id)
        {
            return await _keycardRepository.DeleteKeycardAsync(id);
        }
    }
}
