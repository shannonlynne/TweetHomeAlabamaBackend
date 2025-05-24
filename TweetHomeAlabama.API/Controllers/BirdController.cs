using Microsoft.AspNetCore.Mvc;
using TweetHomeAlabama.Application.Interfaces;
using TweetHomeAlabama.Application.Models;

namespace TweetHomeAlabama.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BirdController : ControllerBase
    {
        private readonly IBirdService _service;

        public BirdController(IBirdService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<BirdDto> dtos = await _service.GetAll();
            return Ok(dtos);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBirds(
            [FromQuery] IEnumerable<string>? colors,
            [FromQuery] string? size,
            [FromQuery] string? shape,
            [FromQuery] IEnumerable<string>? habitats)
        {
            IList<BirdDto> birds = await _service.GetBirds(
                colors ?? new List<string>(),
                size ?? string.Empty,
                shape ?? string.Empty,
                habitats ?? new List<string>());

            return Ok(birds);
        }

        [HttpPost("addbird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto birdDto)
        {
            bool result = false;
            try
            {
                result = await _service.AddBird(birdDto);
                return Ok("Bird added successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}
