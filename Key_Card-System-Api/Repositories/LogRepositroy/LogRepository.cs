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
    }
}
