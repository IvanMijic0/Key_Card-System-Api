using Key_Card_System_Api.Services.LogService;
using Keycard_System_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keycard_System_API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public ActionResult<List<Log>> GetAllLogs()
        {
            return _logService.GetAllLogs();
        }

        [HttpGet("Count/logs")]
        public ActionResult<int> CountLogs()
        {
            return _logService.CountLogs();
        }

        [HttpGet("Count/logs{room_id}")]
        public ActionResult<int> CountLogs(int room_id)
        {
            return _logService.CountLogs(room_id);
        }

        [HttpGet("Count/Errors")]
        public ActionResult<int> CountErrors()
        {
            return _logService.CountErrors();
        }

    }
}
