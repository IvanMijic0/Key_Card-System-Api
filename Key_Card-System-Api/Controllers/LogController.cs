﻿using Key_Card_System_Api.Models.DTO;
using Key_Card_System_Api.Services.LogService;
using Keycard_System_API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keycard_System_API.Controllers
{
    [Authorize]
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

        [HttpGet("RoomsWithCounts")]
        public async Task<ActionResult<List<LogCounts>>> GetCountOflogsWithRoomsAsync()
        {
            try
            {
                var logs = await _logService.GetCountOflogsWithRoomsAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet("Room{id}")]
        public async Task<ActionResult<List<LogDto>>> GetLogsByRoomIdAsync(int id)
        {
            try
            {
                var logs = await _logService.GetLogsByRoomIdAsync(id);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet("User{id}")]
        public async Task<ActionResult<List<LogDto>>> GetLogsLogByUserIdAsync(int id)
        {
            try
            {
                var logs = await _logService.GetLogsByUserIdAsync(id);
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
        public async Task<IActionResult> AddLog(LogRequestModel log)
        {
            var addedLog = await _logService.AddLogAsync(log);
            return CreatedAtAction(nameof(GetAllLogs), addedLog);
        }

        [HttpGet("room")]
        public async Task<ActionResult<List<LogDto>>> GetLogsInRoom()
        {
            var logs = await _logService.GetLogsInRoom();

            if (logs == null || logs.Count == 0)
            {
                return NotFound();
            }

            return logs;
        }

        [HttpGet("search/by-username/{searchTerm}")]
        public async Task<ActionResult<List<LogDto>>> SearchLogsByUser(string searchTerm)
        {
            try
            {
                var logs = await _logService.SearchLogsByUserAsync(searchTerm);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet("search/by-room/{searchTerm}")]
        public async Task<ActionResult<List<LogDto>>> SearchLogsByRoom(string searchTerm)
        {
            try
            {
                var logs = await _logService.SearchLogsByRoomAsync(searchTerm);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet("search/by-keycard/{searchTerm}")]
        public async Task<ActionResult<List<LogDto>>> SearchLogsByKeycard(string searchTerm)
        {
            try
            {
                var logs = await _logService.SearchLogsByKeycardIdAsync(searchTerm);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

    }
}
