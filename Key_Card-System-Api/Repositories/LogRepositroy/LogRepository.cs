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
            return await _context.logs
                .Include(log => log.User)  
                .Include(log => log.Room) 
                .ToListAsync();
        }


        public async Task<Log> AddLogAsync(Log log)
        {
            ArgumentNullException.ThrowIfNull(log);

            _context.logs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task<List<Log>> SearchLogsAsync(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            return await _context.logs
                .Include(log => log.User)
                .Include(log => log.Room)
                .Where(log =>
                    log.User.Username.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                    log.Room.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                    log.Description!.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();
        }
    }
}
