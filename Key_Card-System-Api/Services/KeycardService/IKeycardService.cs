using Key_Card_System_Api.Models;
using Keycard_System_API.Models;

namespace Key_Card_System_Api.Services.KeycardService
{
    public interface IKeycardService
    {
        Task<List<Keycard>> GetAllKeycardsAsync();
        Task<Keycard?> GetKeycardByIdAsync(int id);
        Task CreateKeycardAsync(Keycard keycard);
        Task<Keycard> UpdateKeycardAsync(Keycard keycard);
        Task<User> ReplaceKeycardAsync(int userId);
        Task<bool> DeactivateKeycardAsync(int id);
        Task<bool> DeleteKeycardAsync(int id);
    }
}
