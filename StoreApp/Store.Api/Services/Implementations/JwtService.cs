using Microsoft.IdentityModel.Tokens;
using Store.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Store.Api.Services
{
   
    public class JwtService : IJwtService
    {
        public string GenerateToken(AppUser user, IList<string> roles,IConfiguration confg)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim("FullName",user.FullName),
            };

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
            claims.AddRange(roleClaims);

            string secret = confg.GetSection("JWT:secret").Value;

            var symmetricSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddHours(8),
                issuer: confg.GetSection("JWT:issuer").Value,
                audience: confg.GetSection("JWT:audience").Value
                );

            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenStr;
        }
    }
}
