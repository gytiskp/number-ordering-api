using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Models;
using Web.Services.Sorting;

namespace Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SortingController : ControllerBase
    {
        private readonly ISortingService _service;

        public SortingController(ISortingService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult> Get()
        {
            var result = await _service.GetLatestData();

            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        // No manual ModelState validation because of automatic http 400 responses
        public async Task<ActionResult> Post([FromBody] SortingInput input)
        {
            var result = await _service.Create(input);

            return CreatedAtAction(nameof(Get), result);
        }

    }

}
