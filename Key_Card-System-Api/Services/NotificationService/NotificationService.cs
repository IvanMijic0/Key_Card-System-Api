using Key_Card_System_Api.Models.DTO;
using Key_Card_System_Api.Repositories.NotificationRepository;

namespace Key_Card_System_Api.Services.NotificationService
{
    public class NotificationService : INotificationService
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
                        Id = notification.User.Id,
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
