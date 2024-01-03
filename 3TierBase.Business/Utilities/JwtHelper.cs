using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _3TierBase.Business.Utilities
{

    public interface IJwtHelper
    {
        string GenerateJwtToken(string username, string role, Guid id);
    }
    public class JwtHelper : IJwtHelper
    {
        private readonly IConfiguration _config;

        public JwtHelper(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(string username, string role, Guid id)
        {
            // security key
            string securityKey = _config["JWT:Key"];

            // symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            // signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var claim = new[]{
                new Claim("Username", username),
                new Claim("Id", id.ToString()),
                new Claim("Role", role)
                //new Claim(ClaimTypes.Role, role)
            };

            // create token
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials,
                claims: claim
            );

            //var tokenHandler = new JwtSecurityTokenHandler();

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claim),
            //    Expires = DateTime.Now.AddDays(1),
            //    SigningCredentials = signingCredentials
            //};
            //var tokens = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            //var jwtToken = tokenHandler.WriteToken(tokens);

            // return token
            //return jwtToken;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}