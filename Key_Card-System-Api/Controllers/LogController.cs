using Key_Card_System_Api.Services.LogService;
using Keycard_System_API.Models;
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

        [HttpPost]
        public IActionResult AddLog(Log log)
        {
            try
            {
                var addedLog = _logService.AddLog(log);
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
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}
