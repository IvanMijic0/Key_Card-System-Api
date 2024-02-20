using Keycard_System_API.Data;
using Keycard_System_API.Models;

namespace Key_Card_System_Api.Repositories.LogRepositroy
{
    public class LogRepository(ApplicationDbContext context) : ILogRepository
    {

        private readonly ApplicationDbContext _context = context;

        public List<Log> GetAllLogs()
        {
            return [.. _context.logs];
        }

        public Log AddLog(Log log)
        {
            ArgumentNullException.ThrowIfNull(log);

            _context.logs.Add(log);
            _context.SaveChanges();
            return log;
        }
    }
}
