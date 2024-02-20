﻿using Keycard_System_API.Models;
using Keycard_System_API.Services;
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
    }
}