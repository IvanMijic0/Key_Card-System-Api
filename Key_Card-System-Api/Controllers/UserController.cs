using Key_Card_System_Api.Models.DTO;
using Key_Card_System_Api.Services.UserService;
using Keycard_System_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keycard_System_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("Keycard/{user_id}/{response}/{access_level}")]
        public async Task<IActionResult> UpdateUsersKeyCardAcessLevelAsync(int user_id, string response, string access_level)
        {
            try
            {
                await _userService.UpdateUsersKeyCardAcessLevelAsync(user_id, response, access_level);
                return Ok(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _userService.UpdateUserAsync(user);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);

            if (deleted)
            {
                return Ok(new { message = "Successfully deleted user" });
            }
            else
            {
                return NotFound(new { message = "User not found" });
            }
        }

        [HttpDelete("deactivate/{id}")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var deactivated = await _userService.DeactivateUserAsync(id);

            if (deactivated)
            {
                return Ok(new { message = "Successfully deactivated user" });
            }
            else
            {
                return NotFound(new { message = "User not found" });
            }
        }

        [HttpGet("search/username/{searchTerm}")]
        public async Task<ActionResult<List<User>>> SearchUsersByUsername(string searchTerm)
        {
            var users = await _userService.SearchUsersAsync(searchTerm);
            return Ok(users);
        }

    }
}
