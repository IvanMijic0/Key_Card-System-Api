using Key_Card_System_Api.Services.RoomService;
using Keycard_System_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Keycard_System_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Room>>> GetAllRooms()
        {
            return await _roomService.GetAllRoomsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }

        [HttpPost("{roomId}/accesslevel")]
        public async Task<IActionResult> UpdateRoomAccessLevel(int roomId, string accessLevel)
        {
            try
            {
                await _roomService.UpdateRoomAccessLevelAsync(roomId, accessLevel);
                return Ok("Room access level updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }
    }
}
