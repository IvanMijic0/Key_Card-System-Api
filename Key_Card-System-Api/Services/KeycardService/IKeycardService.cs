using Key_Card_System_Api.Models;

namespace Key_Card_System_Api.Services.KeycardService
{
    public interface IKeycardService
    {
        Task<List<Keycard>> GetAllKeycardsAsync();
        Task<Keycard?> GetKeycardByIdAsync(int id);
        Task CreateKeycardAsync(Keycard keycard);
        Task<Keycard> UpdateKeycardAsync(Keycard keycard);
        Task<bool> DeactivateKeycardAsync(int id);
        Task<bool> DeleteKeycardAsync(int id);
    }
}
