using Key_Card_System_Api.Models.DTO;
using Key_Card_System_Api.Services.NotificationService;
using Microsoft.AspNetCore.Mvc;

namespace Key_Card_System_Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("Request")]
        public async Task<List<NotificationRequest>> GetAllNotificationsWithRequestAsync()
        {
            return await _notificationService.GetAllNotificationsWithRequestAsync();
        }

        [HttpPost]
        public async Task<Notification> AddRequestAsync(NotificationAdd notificationAdd)
        {
            return await _notificationService.AddRequestAsync(notificationAdd);
        }
    }
}
