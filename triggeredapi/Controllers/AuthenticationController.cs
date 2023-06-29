using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using triggeredapi.Models;
using triggeredapi.Repo;
using triggeredapi.Service;
using triggeredapi.Utils;

namespace triggeredapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController: ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AccessTokenGenerator _tokenGenerator;

        public AuthenticationController(IUserRepository userRepository, IPasswordHasher passwordHasher, AccessTokenGenerator tokenGenerator)
        {
            _userRepo = userRepository;
            _passwordHasher  = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if(!ModelState.IsValid){
                return BadRequest("Username and/or password is empty");
            }
            var user = await _userRepo.GetByUserName(registerRequest.Username);
            if(user!= null) return Conflict("Username already exists");
            User newUser = new User()
            {
                Username = registerRequest.Username,
                PasswordHash = _passwordHasher.HashPassword(registerRequest.Password),
                TelegramId = registerRequest.TelegramId
            };
            await _userRepo.Create(newUser);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid) return BadRequest();
            User user = await _userRepo.GetByUserName(login.Username);
            if(user==null) return Unauthorized();
            bool isCorrectPassword = _passwordHasher.VerifyPassword(login.Password, user.PasswordHash);
            if (!isCorrectPassword) return Unauthorized();
            string accessToken = _tokenGenerator.GenerateToken(user);
            return Ok(accessToken);
        }

        [HttpGet("try")]
        public async Task<IActionResult> GetNovel()
        {
            Console.WriteLine("HIT BREAKPOINT");
            return Ok();
        }

    }
}