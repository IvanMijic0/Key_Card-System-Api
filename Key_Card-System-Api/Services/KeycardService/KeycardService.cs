using Key_Card_System_Api.Models;
using Key_Card_System_Api.Repositories.KeycardRepository;

namespace Key_Card_System_Api.Services.KeycardService
{
    public class KeycardService : IKeycardService
    {
        private readonly IKeycardRepository _keycardRepository;

        public KeycardService(IKeycardRepository keycardRepository)
        {
            _keycardRepository = keycardRepository ?? throw new ArgumentNullException(nameof(keycardRepository));
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
