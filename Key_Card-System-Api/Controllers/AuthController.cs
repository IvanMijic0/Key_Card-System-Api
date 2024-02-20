using Keycard_System_API.Models;
using Keycard_System_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Keycard_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            bool isEmail = model.Username.Contains('@');

            var user = isEmail ? await _userService.AuthenticateByEmail(model.Username, model.Password) :
                                 await _userService.AuthenticateByUsername(model.Username, model.Password);

            if (user == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("E8TmjOvUoSMkvbvw3nU7nMps1T+8W+mBc9s+7/X9SG0=");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new(ClaimTypes.Name, user.Id.ToString()),
                   new(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Token = tokenString
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            string defaultRole = "Employee";

            var newUser = new User(model.Username, model.Email, model.Password);

            if (!string.IsNullOrEmpty(model.Role))
            {
                newUser.Role = model.Role;
            }
            else
            {
                newUser.Role = defaultRole;
            }

            var registeredUser = await _userService.Register(newUser, model.Password);

            if (registeredUser == null)
            {
                return BadRequest("Failed to register user. Username may already exist.");
            }

            return Ok("User registered successfully");
        }

    }
}
