using Key_Card_System_Api.Models;
using Keycard_System_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Key_Card_System_Api.Repositories.NotificationRepository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetAllNotificationsWithRequestAsync()
        {
            return await _context.notifications.Where(n => n.Type == "request")
               .Include(log => log.User)
               .ToListAsync();
        }
    }
}
