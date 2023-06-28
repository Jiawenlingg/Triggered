using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using triggeredapi.Models;

namespace triggeredapi.Service
{
    public class AccessTokenGenerator
    {
        public string GenerateToken(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("randomsecretkeyhjkugkugkgkgifififjlfjulfjufjlufulfujlfilfilfglfglhjgljkgS"));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>(){
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            JwtSecurityToken token = new JwtSecurityToken("http://localhost:5001", "http://localhost:5001", claims, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30), credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}