using Key_Card_System_Api.Models;
using Key_Card_System_Api.Repositories.KeycardRepository;

namespace Key_Card_System_Api.Services.KeycardService
{
    public class KeycardService : IKeycardService
    {
        private readonly IKeycardRepository _keycardRepository;

        public KeycardService(IKeycardRepository keycardRepository)
        {
            _keycardRepository = keycardRepository;
        }

        public List<Keycard> GetAllKeycards()
        {
            return _keycardRepository.GetAllKeycards();
        }

        public Keycard? GetKeycardById(int id)
        {
            return _keycardRepository.GetKeycardById(id);
        }

        public void CreateKeycard(Keycard keycard)
        {
            ArgumentNullException.ThrowIfNull(keycard);
            _keycardRepository.CreateKeycard(keycard);
        }

        public Keycard UpdateKeycard(Keycard keycard)
        {
            ArgumentNullException.ThrowIfNull(keycard);
            return _keycardRepository.UpdateKeycard(keycard);
        }

        public bool DeleteKeycard(int id)
        {
            return _keycardRepository.DeleteKeycard(id);
        }
    }
}
