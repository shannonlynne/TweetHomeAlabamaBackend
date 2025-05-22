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

        [HttpPost]
        public async Task<IActionResult> AddBird([FromBody] BirdDto birdDto)
        {
            bool result = await _service.AddBird(birdDto);
            return !result ? StatusCode(500, "Could not save the bird.") : (IActionResult)Ok("Bird added.");
        }
    }
}
