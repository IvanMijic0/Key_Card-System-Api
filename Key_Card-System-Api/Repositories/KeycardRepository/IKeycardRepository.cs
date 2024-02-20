using Key_Card_System_Api.Models;

namespace Key_Card_System_Api.Repositories.KeycardRepository
{
    public interface IKeycardRepository
    {
        List<Keycard> GetAllKeycards();
        Keycard? GetKeycardById(int id);
        void CreateKeycard(Keycard keycard);
        Keycard UpdateKeycard(Keycard keycard);
        bool DeleteKeycard(int id);
    }
}
