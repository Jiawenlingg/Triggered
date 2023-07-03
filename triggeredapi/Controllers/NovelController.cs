using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using triggeredapi.Service;

namespace triggeredapi.Controllers
{
    [ApiController]
    [Route("Novel")]
    public class NovelController: ControllerBase
    {
        private readonly NovelParser _novelParser;
        public NovelController(NovelParser novelParser)
        {
            _novelParser = novelParser;
        }
        [HttpGet]
        public IActionResult GetNovel(string search, string searchSite)
        {
            if(!NovelSite.TryParse(searchSite, out NovelSite site)) return BadRequest("Site is invalid");
            var result = _novelParser.GetNovel(site, search);
            return Ok(result);
        }
    }
}