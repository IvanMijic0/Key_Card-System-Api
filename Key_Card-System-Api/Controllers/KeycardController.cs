using Key_Card_System_Api.Models;
using Key_Card_System_Api.Services.KeycardService;
using Microsoft.AspNetCore.Mvc;

namespace Key_Card_System_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeycardController : ControllerBase
    {
        private readonly IKeycardService _keycardService;

        public KeycardController(IKeycardService keycardService)
        {
            _keycardService = keycardService ?? throw new ArgumentNullException(nameof(keycardService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKeycards()
        {
            try
            {
                var keycards = await _keycardService.GetAllKeycardsAsync();
                return Ok(keycards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKeycardById(int id)
        {
            try
            {
                var keycard = await _keycardService.GetKeycardByIdAsync(id);
                if (keycard == null)
                {
                    return NotFound();
                }
                return Ok(keycard);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateKeycard([FromBody] Keycard keycard)
        {
            try
            {
                await _keycardService.CreateKeycardAsync(keycard);
                return CreatedAtAction(nameof(GetKeycardById), new { id = keycard.Id }, keycard);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("replace/{userId}")]
        public async Task<IActionResult> ReplaceKeycard(int userId)
        {
            try
            {
                var user = await _keycardService.ReplaceKeycardAsync(userId);
                return Ok(user);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKeycard(int id, [FromBody] Keycard keycard)
        {
            try
            {
                if (id != keycard.Id)
                {
                    return BadRequest("Mismatched id in route and body");
                }

                var updatedKeycard = await _keycardService.UpdateKeycardAsync(keycard);
                return Ok(updatedKeycard);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deactivate/{id}")]
        public async Task<IActionResult> DeactivateKeycard(int id)
        {
            try
            {
                var result = await _keycardService.DeactivateKeycardAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKeycard(int id)
        {
            try
            {
                var result = await _keycardService.DeleteKeycardAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
