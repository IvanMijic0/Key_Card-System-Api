﻿using Key_Card_System_Api.Models.DTO;
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

        [HttpPost]
        public async Task<IActionResult> AddLog(LogRequestModel log)
        {
            var addedLog = await _logService.AddLogAsync(log);
                return CreatedAtAction(nameof(GetAllLogs), addedLog);
          }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<List<LogDto>>> SearchLogs(string searchTerm)
        {
            try
            {
                var logs = await _logService.SearchLogsAsync(searchTerm);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}
