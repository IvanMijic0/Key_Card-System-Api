using Key_Card_System_Api.Models;

namespace Key_Card_System_Api.Services.KeycardService
{
    public interface IKeycardService
    {
        List<Keycard> GetAllKeycards();
        Keycard? GetKeycardById(int id);
        void CreateKeycard(Keycard keycard);
        Keycard UpdateKeycard(Keycard keycard);
        bool DeleteKeycard(int id);
    }
}
