using Key_Card_System_Api.Models;
using Key_Card_System_Api.Models.DTO;
using Key_Card_System_Api.Services.KeycardService;
using Key_Card_System_Api.Services.UserService;
using Keycard_System_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Keycard_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserService userService, IKeycardService keycardService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IKeycardService _keycardService = keycardService;

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
                   new(ClaimTypes.NameIdentifier, user.Id.ToString()),
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

            var newKeycard = new Keycard(model.Key_Id, model.Access_Level);

           _keycardService.CreateKeycard(newKeycard);

            var newUser = new User(model.Username, model.Email, newKeycard.Id, model.Password)
            {
                Role = !string.IsNullOrEmpty(model.Role) ? model.Role : defaultRole
            };

            var registeredUser = await _userService.Register(newUser, model.Password);

            if (registeredUser == null)
            {
                _keycardService.DeleteKeycard(newKeycard.Id);
                return BadRequest("Failed to register user. Username may already exist.");
            }

            return Ok("User registered successfully");
        }


    }
}
