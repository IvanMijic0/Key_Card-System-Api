using Key_Card_System_Api.Models;
using Key_Card_System_Api.Models.DTO;

namespace Key_Card_System_Api.Services.NotificationService
{
    public interface INotificationService
    {
        Task<List<NotificationRequest>> GetAllNotificationsWithRequestAsync();
    }
}
