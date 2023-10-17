using Dor.Blog.Application.Authorization;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dor.Blog.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;

        }
        
        public async Task<User> Authenticate(Credential credential)
        {
            //Search for user filtering by userName
            var user = await _unitOfWork.UserRepository.GetUserByUserName(credential.UserName);

            if (user == null)
            {
                var error = new InvalidOperationException("User doesn't exists");
                return await Task.FromException<User>(error);

            }

            var res = await _unitOfWork.AuthenticationRepository.CheckUsernameAndPassword(user, credential.Password);

            if (!res.Succeeded)
            {
                var error = new InvalidOperationException("Username or Password doesn't match.");
                return await Task.FromException<User>(error);
            }

            //generate token            
            string secretKey = _config.GetSection("SecretKey").Value ?? "";           

            //user.token = GenerateJwtToken(user.UserName??"",secretKey);
            user.token = await GenerateJwtToken(user, secretKey);

            return user;
        }

        private async Task<string> GenerateJwtToken(User user, string secret)
        {
            var secretKey = Encoding.UTF8.GetBytes("RodrigoPaola.2905");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName??"")
            };

            // generate the JWT
            var jwt = new JwtSecurityToken(
                    //claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.Now.AddHours(30),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(secretKey),
                        SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }     

    }
}
