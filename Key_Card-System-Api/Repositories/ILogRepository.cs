using Keycard_System_API.Models;

namespace Keycard_System_API.Repositories
{
    public interface ILogRepository
    {
        List<Log> GetAllLogs();
    }
}
