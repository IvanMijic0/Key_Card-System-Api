using Keycard_System_API.Models;
using Keycard_System_API.Models.DTO;

namespace Key_Card_System_Api.Repositories.LogRepositroy
{
    public interface ILogRepository
    {
        Task<List<Log>> GetAllLogsAsync();
        Task<Log> AddLogAsync(Log log);
        Task<List<Log>> SearchLogsAsync(string searchTerm);
        Task<List<Log>> GetLogsByRoomIdAsync(int room_id);
        Task<List<Log>> GetLogsByUserIdAsync(int user_id);
        Task<int> CountLogsAsync();
        Task<int> CountLogsAsync(int room_id);
        Task<int> CountErrorsAsync();
    }
}
