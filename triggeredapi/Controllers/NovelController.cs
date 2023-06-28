using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace triggeredapi.Controllers
{
    [ApiController]
    [Route("Novel")]
    public class NovelController: ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetNovel()
        {
            return Ok();
        }
    }
}