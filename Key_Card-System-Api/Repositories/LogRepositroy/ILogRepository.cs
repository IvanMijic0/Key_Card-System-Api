using Keycard_System_API.Models;

namespace Key_Card_System_Api.Repositories.LogRepositroy
{
    public interface ILogRepository
    {
        Task<List<Log>> GetAllLogsAsync();
        Task<Log> AddLogAsync(Log log);
        Task<List<Log>> SearchLogsAsync(string searchTerm);
      
        Task<int> CountLogsAsync();
        Task<int> CountLogsAsync(int room_id);
        Task<int> CountErrorsAsync();
    }
}
