using Keycard_System_API.Data;
using Keycard_System_API.Models;
using Keycard_System_API.Models.DTO;
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

        public async Task<int> CountLogsAsync()
        {
            DateTime sevenDaysAgo = DateTime.Now.AddDays(-8);
            int logCount = await _context.logs.CountAsync(log => log.Timestamp >= sevenDaysAgo);
            return logCount;
        }

        public async Task<int> CountLogsAsync(int room_id)
        {
            DateTime sevenDaysAgo = DateTime.Now.AddDays(-8);
            int logCount = await _context.logs.CountAsync(log => log.Timestamp >= sevenDaysAgo && log.Room_id == room_id);
            return logCount;
        }

        public async Task<int> CountErrorsAsync()
        {
            int errorCount = await _context.logs.CountAsync(log => log.Entry_type == "Error");
            return errorCount;
        }

        public async Task<List<Log>> GetAllLogsAsync()
        {
            return await _context.logs
                .Include(log => log.User)
                .Include(log => log.Room)
                .ToListAsync();
        }
        public async Task<List<Log>> GetLogsByRoomIdAsync(int room_id)
        {
            return await _context.logs
               .Where(log => log.Room_id == room_id)
               .Include(log => log.User)
               .Include(log => log.Room)
               .ToListAsync();
        }

        public async Task<List<Log>> GetLogsByUserIdAsync(int user_id)
        {
            return await _context.logs
                .Where(log => log.User_id == user_id)
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
