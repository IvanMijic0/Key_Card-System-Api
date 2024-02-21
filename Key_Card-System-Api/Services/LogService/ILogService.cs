using Key_Card_System_Api.Models.DTO;
using Keycard_System_API.Models;
using Keycard_System_API.Models.DTO;

namespace Key_Card_System_Api.Services.LogService
{
    public interface ILogService
    {
        Task<List<LogDto>> GetAllLogsAsync();
        Task<Log> AddLogAsync(LogRequestModel logRequest);
        Task<List<Log>> SearchLogsAsync(string searchTerm);
        Task<List<LogDto>> GetLogsInRoom();
        Task<int> CountLogsAsync();
        Task<int> CountLogsAsync(int room_id);
        Task<int> CountErrorsAsync();
    }
}
