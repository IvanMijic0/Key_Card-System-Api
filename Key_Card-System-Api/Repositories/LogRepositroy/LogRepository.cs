using Keycard_System_API.Data;
using Keycard_System_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Keycard_System_API.Repositories
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
