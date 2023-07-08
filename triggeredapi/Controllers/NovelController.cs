using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using triggeredapi.Service;
using triggeredapi.Models;
using triggeredapi.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace triggeredapi.Controllers
{
    [ApiController]
    [Route("Novel")]
    public class NovelController: ControllerBase
    {
        private readonly NovelParser _novelParser;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public NovelController(NovelParser novelParser, DataContext dataContext, IMapper mapper, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _novelParser = novelParser;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _dataContext = dataContext;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetNovel([FromQuery]string search)
        {
            try
            {
                var result = _novelParser.GetNovel(search);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult GetAllNovels()
        {
            try
            {
                var username = User.Identity.Name;
                var user = _userManager.Users.Include(x=> x.Novels).First(x=> x.UserName== username);
                if (!user.Novels.Any()) return NoContent();
                return Ok(user.Novels.Select(x=> new NovelResult(){

                    Title = x.Title,
                    Website = x.Website,
                    Url = x.Url,
                    Image = x.Image,
                    LastUpdate = x.LastUpdate,
                    LatestChapter = x.LatestChapter 
                }
                ));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("save")]
        [Authorize]
        public async Task<IActionResult> SaveNovels([FromBody]List<NovelResult> saveResults)
        {
            var username = User.Identity.Name;
            var user =  _userManager.Users.Include(x=> x.Novels).Single(x=> x.UserName==username);
            foreach(var result in saveResults)
            {
                var novel = _dataContext.Novel.FirstOrDefault(x=> x.Title.Equals(result.Title) && x.Website.Equals(result.Website));
                if(novel == null)
                {
                    novel = _mapper.Map<Novel>(result);                  
                }
                
                user.Novels.Add(novel);
            }
            int saved = await _dataContext.SaveChangesAsync();
            
            return Ok($"Novels saved!");

        }

        [HttpPost("delete")]
        [Authorize]
        public async Task<IActionResult> RemoveNovels([FromBody]List<NovelResult> saveResults)
        {
            var username = User.Identity.Name;
            var user = _userManager.Users.Include(x=> x.Novels).Single(x=> x.UserName == username);
            var count = 0;
            foreach(var result in saveResults)
            {
                var novel = _dataContext.Novel.FirstOrDefault(x=> x.Title.Equals(result.Title)&& x.Website.Equals(result.Website));
                if(novel == null) continue;
                user.Novels.Remove(novel);
                count++;
            }
            await _dataContext.SaveChangesAsync();
            return Ok($"{count} Novels removed!");

        }
    }
}