using Key_Card_System_Api.Models;


namespace Key_Card_System_Api.Repositories.KeycardRepository
{
    public interface IKeycardRepository
    {
        Task<List<Keycard>> GetAllKeycardsAsync();
        Task<Keycard?> GetKeycardByIdAsync(int id);
        Task CreateKeycardAsync(Keycard keycard);
        Task<Keycard> UpdateKeycardAsync(Keycard keycard);
        Task<bool> DeleteKeycardAsync(int id);
    }
}
