using Key_Card_System_Api.Models;

namespace Key_Card_System_Api.Repositories.NotificationRepository
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetAllNotificationsWithRequestAsync();
    }
}
