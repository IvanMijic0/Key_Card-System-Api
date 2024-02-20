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
        public ActionResult<List<User>> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _userService.UpdateUser(user);

            return user;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deleted = _userService.DeleteUser(id);

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
        public IActionResult DeactivateUser(int id)
        {
            var deactivated = _userService.DeactivateUser(id);

            if (deactivated)
            {
                return Ok(new { message = "Successfully deactivated user" });
            }
            else
            {
                return NotFound(new { message = "User not found" });
            }
        }

    }
}
