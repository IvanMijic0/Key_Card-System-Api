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
            return await _context.notifications
               .Include(notification => notification.User)
               .ToListAsync();
        }

        public async Task<Notification> AddRequestAsync(Notification notification)
        {
            ArgumentNullException.ThrowIfNull(notification);

            _context.notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }
    }
}
