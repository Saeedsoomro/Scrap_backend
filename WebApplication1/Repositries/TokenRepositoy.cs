using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.Repositries
{
    public class TokenRepositoy : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepositoy(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CraeteJwtToken(IdentityUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
  
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
           issuer: configuration["Jwt:Issuer"],
           audience: configuration["Jwt:Audience"],
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(30), // Adjust token expiration as needed
           signingCredentials: creds
             );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
