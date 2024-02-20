using Key_Card_System_Api.Repositories.LogRepositroy;
using Keycard_System_API.Models;

namespace Key_Card_System_Api.Services.LogService
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public List<Log> GetAllLogs()
        {
            return _logRepository.GetAllLogs();
        }

    }
}
