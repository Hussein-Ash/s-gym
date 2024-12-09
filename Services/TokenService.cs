using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using e_parliament.Interface;
using EvaluationBackend.DATA.DTOs.User;
using EvaluationBackend.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace EvaluationBackend.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                // add role Claim
                new Claim("id", user.Id.ToString()),
                // new Claim("FullName",user.FullName!),
                new Claim("Role", user.Role!),
                new Claim("ExpierDate", DateTime.UtcNow.AddMinutes(15).ToString()),
                // new Claim(JwtRegisteredClaimNames., user.Email.ToString()),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        #region not from structure
        public string CreateRefreshToken(UserDto user)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                // add role Claim
                new Claim("id", user.Id.ToString()),
                new Claim("ExpierDate", DateTime.UtcNow.AddMinutes(15).ToString()),
                // new Claim(JwtRegisteredClaimNames., user.Email.ToString()),
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion
    
    }
}