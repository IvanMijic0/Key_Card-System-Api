using Keycard_System_API.Models;

namespace Key_Card_System_Api.Repositories.LogRepositroy
{
    public interface ILogRepository
    {
        List<Log> GetAllLogs();
        Log AddLog(Log log);
    }
}
