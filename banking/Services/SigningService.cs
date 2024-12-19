using banking.Models;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace banking.Services
{
    public class SigningService
    {
        private readonly Jwt _jwt;
        private readonly IHttpContextAccessor _contextAccessor;

        public SigningService(Jwt jwt, IHttpContextAccessor contextAccessor)
        {
            _jwt = jwt;
            _contextAccessor = contextAccessor;
        }

        protected string generateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey))
                , SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddSeconds(5),
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Name,user.userName),
                    new (ClaimTypes.Email,user.email),
                    new (ClaimTypes.Role,user.role.ToString()),
                    new(ClaimTypes.NameIdentifier,user.id.ToString())
                })
            };
            Console.WriteLine(tokenDescriptor.Expires);
            Console.WriteLine(DateTime.Now);
            if (DateTime.Now > tokenDescriptor.Expires)
            {
                throw new ArgumentException("token is expired");
                //var randomNumber = new byte[32];
                //var rng = RandomNumberGenerator.Create();
                //rng.GetBytes(randomNumber);
                //accessToken=Convert.ToBase64String(randomNumber);
            }
            var securityToken=tokenHandler.CreateToken(tokenDescriptor);
            var accessToken=tokenHandler.WriteToken(securityToken);
            Console.WriteLine(accessToken);
            return accessToken;
        }

        private string generateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        protected string hashPassword(string password) 
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        protected bool verifyPassword(string password,string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public int getCurrentUserId()
        {
            if (_contextAccessor?.HttpContext?.User == null)
            {
                throw new ArgumentException("context accessor user is not found");
            }
            var stringCurrentUserId = _contextAccessor.HttpContext.User.FindFirst(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
            if (stringCurrentUserId == null)
            {
                throw new ArgumentException("user id is not found");
            }
            return int.Parse(stringCurrentUserId);
        }
    }
}
