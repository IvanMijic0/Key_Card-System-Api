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

        public int CountLogs()
        {
            DateTime sevenDaysAgo = DateTime.Now.AddDays(-8);
            int logCount = _context.logs.Count(log => log.Timestamp >= sevenDaysAgo);
            return logCount;
        }

        public int CountLogs(int room_id)
        {
            DateTime sevenDaysAgo = DateTime.Now.AddDays(-8);
            int logCount = _context.logs.Count(log => log.Timestamp >= sevenDaysAgo && log.Room_id == room_id);
            return logCount;
        }

        public int CountErrors()
        {
            int errorCount = _context.logs.Count(log => log.Entry_type == "Error");
            return errorCount;
        }
    }
}
