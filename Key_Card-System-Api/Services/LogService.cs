using Keycard_System_API.Models;
using Keycard_System_API.Repositories;

namespace Keycard_System_API.Services
{
    public class LogService:ILogService
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
