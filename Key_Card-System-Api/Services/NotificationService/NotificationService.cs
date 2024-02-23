using Key_Card_System_Api.Models;
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
                if (notification.Is_active == 1)
                {
                    var notificationRequest = new NotificationRequest
                    {
                        Id = notification.User.Id,
                        UserEmail = notification.User.Email,
                        UserFirstName = notification.User.FirstName,
                        UserSecondName = notification.User.LastName,
                        Access_level = notification.Access_level
                    };
                    notificationRequests.Add(notificationRequest);
                }
            }

            return notificationRequests;
        }

        public async Task<Notification> AddRequestAsync(NotificationAdd notificationAdd)
        {
            ArgumentNullException.ThrowIfNull(notificationAdd);
            var notification = new Notification();

            if (notificationAdd.Type_of_request == "access_level")
            {
                notification = new Notification(0, notificationAdd.User_Id, "Request for higher access level",
                    notificationAdd.Type_of_request, notificationAdd.Access_level, 1);
            }
            else
            {
                notification = new Notification(0, notificationAdd.User_Id, "Request for new keycard",
                     notificationAdd.Type_of_request, "", 1);
            }

            await _notificationRepository.AddRequestAsync(notification);

            return notification;
        }
    }
}
