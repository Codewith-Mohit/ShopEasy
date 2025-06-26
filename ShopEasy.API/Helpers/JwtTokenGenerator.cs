using Humanizer;
using Microsoft.IdentityModel.Tokens;
using ShopEasy.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopEasy.API.Helpers
{
    // JWT is stateless: the server doesn’t need to remember sessions. All info is in the token.
    
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _config;

        public JwtTokenGenerator(IConfiguration config)
        {
            _config = config;
        }
        
        public string GenerateToken(User user)
        {
            var claims = new[]
            {                
               
                //Adds the username to the token payload
                new Claim(ClaimTypes.Name, user.Username),
                //Adds the role (e.g., Admin/Customer)
                new Claim(ClaimTypes.Role, user.Role)


            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            
            //Signs the token using a secret key and algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Creates the actual JWT object
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            
            //Converts the token to a string you send to the client
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
