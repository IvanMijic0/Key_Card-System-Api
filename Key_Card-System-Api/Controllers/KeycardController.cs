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
        public IActionResult GetAllKeycards()
        {
            var keycards = _keycardService.GetAllKeycards();
            return Ok(keycards);
        }

        [HttpGet("{id}")]
        public IActionResult GetKeycardById(int id)
        {
            try
            {
                var keycard = _keycardService.GetKeycardById(id);
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
        public IActionResult CreateKeycard([FromBody] Keycard keycard)
        {
            try
            {
                _keycardService.CreateKeycard(keycard);
                return CreatedAtAction(nameof(GetKeycardById), new { id = keycard.Id }, keycard);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateKeycard(int id, [FromBody] Keycard keycard)
        {
            try
            {
                if (id != keycard.Id)
                {
                    return BadRequest("Mismatched id in route and body");
                }

                var updatedKeycard = _keycardService.UpdateKeycard(keycard);
                return Ok(updatedKeycard);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deactivate/{id}")]
        public IActionResult DeactivateKeycard(int id)
        {
            try
            {
                var result = _keycardService.DeactivateKeycard(id);
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
        public IActionResult DeleteKeycard(int id)
        {
            try
            {
                var result = _keycardService.DeleteKeycard(id);
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
