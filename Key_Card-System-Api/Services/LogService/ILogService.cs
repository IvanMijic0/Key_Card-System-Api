using Key_Card_System_Api.Models.DTO;
using Keycard_System_API.Models;
using Keycard_System_API.Models.DTO;

namespace Key_Card_System_Api.Services.LogService
{
    public interface ILogService
    {
        Task<List<LogDto>> GetAllLogsAsync();
        Task<List<LogCounts>> GetCountOflogsWithRoomsAsync();
        Task<Log> AddLogAsync(LogRequestModel logRequest);
        Task<List<Log>> SearchLogsByUserAsync(string searchTerm);
        Task<List<Log>> SearchLogsByRoomAsync(string searchTerm);
        Task<List<Log>> SearchLogsByKeycardIdAsync(string searchTerm);
        Task<List<LogDto>> GetLogsInRoom();
        Task<List<LogDto>> GetLogsByRoomIdAsync(int room_id);
        Task<List<LogDto>> GetLogsByUserIdAsync(int user_id);
        Task<int> CountLogsAsync();
        Task<int> CountLogsAsync(int room_id);
        Task<int> CountErrorsAsync();
    }
}
