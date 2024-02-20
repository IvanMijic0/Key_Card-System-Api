using Keycard_System_API.Models;
using Keycard_System_API.Models.DTO;

namespace Key_Card_System_Api.Services.LogService
{
    public interface ILogService
    {
        Task<List<LogDto>> GetAllLogsAsync();
        Task<Log> AddLogAsync(Log log);
    }
}
