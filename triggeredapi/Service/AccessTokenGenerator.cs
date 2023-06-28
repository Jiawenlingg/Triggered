using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using triggeredapi.Models;

namespace triggeredapi.Service
{
    public class AccessTokenGenerator
    {
        private IConfiguration _configuration;
        public AccessTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>(){
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            JwtSecurityToken token = new JwtSecurityToken(_configuration["JwtSettings:Issuer"], _configuration["JwtSettings:Audience"], claims, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:Expiration"])), credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}