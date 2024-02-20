using Keycard_System_API.Models;
using Keycard_System_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Keycard_System_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController:ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public ActionResult<List<Room>> GetAllRooms()
        {
            return _roomService.GetAllRooms();
        }

        [HttpGet("{id}")]
        public ActionResult<Room> GetRoomById(int id)
        {
           var room = _roomService.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }
    }
}
