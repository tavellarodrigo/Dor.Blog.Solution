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

            var res = await _unitOfWork.AuthenticationRepository.CheckPasswordSignInAsync(user, credential.Password);

            if (!res.Succeeded)
            {
                var error = new InvalidOperationException("Username or Password doesn't match.");
                return await Task.FromException<User>(error);
            }

            //generate token
            user.token = await GenerateJwtToken(user);

            return user;
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            string secretKey = _config.GetSection("JWT:SecretKey").Value ?? "";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName??"")
            };

            foreach (var role in user.RoleNames){
                claims.Add(new Claim(ClaimTypes.Role, user.RoleNames.FirstOrDefault() ?? ""));
            }
            

            // generate the JWT
            var jwt = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }     

    }
}
