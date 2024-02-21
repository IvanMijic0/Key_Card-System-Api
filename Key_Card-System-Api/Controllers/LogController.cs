using Key_Card_System_Api.Services.LogService;
using Keycard_System_API.Models;
using Keycard_System_API.Models.DTO;
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
        public async Task<ActionResult<List<LogDto>>> GetAllLogs()
        {
            try
            {
                var logs = await _logService.GetAllLogsAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet("{room_id}")]
        public async Task<ActionResult<List<Log>>> GetLogsByRoomIdAsync(int room_id)
        {
            try
            {
                var logs = await _logService.GetLogsByRoomIdAsync(room_id);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet("Count/logs")]
        public async Task<ActionResult<int>> CountLogsAsync()
        {
            return await _logService.CountLogsAsync();
        }

        [HttpGet("Count/logs{room_id}")]
        public async Task<ActionResult<int>> CountLogsAsync(int room_id)
        {
            return await _logService.CountLogsAsync(room_id);
        }

        [HttpGet("Count/Errors")]
        public async Task<ActionResult<int>> CountErrorsAsync()
        {
            return await _logService.CountErrorsAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddLog(Log log)
        {
            try
            {
                var addedLog = await _logService.AddLogAsync(log);
                return CreatedAtAction(nameof(GetAllLogs), addedLog);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(401, new { message = "Invalid Access" });
            }
        }
    }
}
