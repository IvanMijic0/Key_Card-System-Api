using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keycard_System_API.Data;
using Keycard_System_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Key_Card_System_Api.Repositories.LogRepositroy
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationDbContext _context;

        public LogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Log>> GetAllLogsAsync()
        {
            return await _context.logs.ToListAsync();
        }

        public async Task<Log> AddLogAsync(Log log)
        {
            ArgumentNullException.ThrowIfNull(log);

            _context.logs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }
    }
}
