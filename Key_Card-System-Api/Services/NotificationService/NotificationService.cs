using Key_Card_System_Api.Models;
using Key_Card_System_Api.Models.DTO;
using Key_Card_System_Api.Repositories.NotificationRepository;
using Key_Card_System_Api.Repositories.RoomRepository;
using Keycard_System_API.Models;

namespace Key_Card_System_Api.Services.NotificationService
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<NotificationRequest>> GetAllNotificationsWithRequestAsync()
        {
            var notifications = await _notificationRepository.GetAllNotificationsWithRequestAsync();
            var notificationRequests = new List<NotificationRequest>();

            foreach (var notification in notifications)
            {
                if (notification.Type == "request" && notification.Is_active == 1)
                {
                    var notificationRequest = new NotificationRequest
                    {
                        UserFirstName = notification.User.FirstName,
                        UserSecondName = notification.User.LastName,
                        Access_level = notification.Access_level
                    };
                    notificationRequests.Add(notificationRequest);
                }
            }

            return notificationRequests;
        }
    }
}
